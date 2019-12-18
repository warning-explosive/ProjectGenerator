namespace SpaceEngineers.ProjectGenerator
{
    using Core.CompositionRoot.Attributes;
    using Core.CompositionRoot.Enumerations;
    
    [Lifestyle(EnLifestyle.Singleton)]
    internal class AppveyorGenerator : SolutionConfigurationFileGeneratorBase
    {
        protected override string FileName => "appveyor.yml";

        protected override string Content(SolutionInformation solutionInfo) =>
$@"#---------------------------------#
#      general configuration      #
#---------------------------------#

# branches to build
branches:
  # GitFlow whitelist
  only:
    - master
    - develop
    - /release-(\d)+.(\d)+.(\d)+/
    - /feature-(\d)+/
    - /hotfix-(\d)+.(\d)+.(\d)+/
    - /support-(\d)+.(\d)+.(\d)+/
    - /pull-request-(\d)+/

# Do not build on tags (GitHub, Bitbucket, GitLab, Gitea)
skip_tags: false

# Start builds on tags only (GitHub, BitBucket, GitLab, Gitea)
skip_non_tags: false

# Skipping commits with particular message
# Regex for matching commit message
skip_commits:
  message: /\[DoNotBuild\]/

# Do not build feature branch with open Pull Requests
skip_branch_with_pr: false

# Maximum number of concurrent jobs for the project
max_jobs: 1

#---------------------------------#
#    environment configuration    #
#---------------------------------#

# Build worker image (VM template)
image: Visual Studio 2019

# scripts that are called at very beginning, before repo cloning
init:
  - git config --global core.autocrlf true

# clone directory
clone_folder: c:\projects\{solutionInfo.SolutionName}

# clone entire repository history if not defined
shallow_clone: false

# scripts that run after cloning repository
install:
  - choco install gitversion.portable --version 4.0.0 -y

# enable patching of AssemblyInfo.* files
assembly_info:
  patch: false

# Automatically register private account and/or project AppVeyor NuGet feeds.
nuget:
  account_feed: true
  project_feed: true
  # disable publishing of .nupkg artifacts to account/project feeds for pull request builds
  disable_publish_on_pr: true

#---------------------------------#
#       build configuration       #
#---------------------------------#

# build platform, i.e. x86, x64, Any CPU. This setting is optional.
platform: Any CPU

# build Configuration, i.e. Debug, Release, etc.
configuration:
  - Debug
  - Release

matrix:
  fast_finish: true

for:
# release job filter
-
  matrix:
    only:
      - configuration: Release

  branches:
    only:
      - master
      - /release-(\d)+.(\d)+.(\d)+/
      - /hotfix-(\d)+.(\d)+.(\d)+/
      - /support-(\d)+.(\d)+.(\d)+/

# debug job filter
-
  matrix:
    only:
      - configuration: Debug

  branches:
    only:
      - develop
      - /feature-(\d)+/
      - /pull-request-(\d)+/

# Build settings, not to be confused with ""before_build"" and ""after_build"".
# ""project"" is relative to the original build directory and not influenced by directory changes in ""before_build"".
build:
  parallel: true                                                                # enable MSBuild parallel builds
  project: {solutionInfo.SolutionName}.sln                                      # path to Visual Studio solution or project
  publish_wap: false                                                            # package Web Application Projects (WAP) for Web Deploy
  publish_wap_xcopy: false                                                      # package Web Application Projects (WAP) for XCopy deployment
  publish_wap_beanstalk: false                                                  # Package Web Applications for AWS Elastic Beanstalk deployment
  publish_wap_octopus: false                                                    # Package Web Applications for Octopus deployment
  publish_azure_webjob: false                                                   # Package Azure WebJobs for Zip Push deployment
  publish_azure: false                                                          # package Azure Cloud Service projects and push to artifacts
  publish_aspnet_core: false                                                    # Package ASP.NET Core projects
  publish_core_console: false                                                   # Package .NET Core console projects
  publish_nuget: false                                                          # package projects with .nuspec files and push to artifacts
  publish_nuget_symbols: false                                                  # generate and publish NuGet symbol packages
  include_nuget_references: false                                               # add -IncludeReferencedProjects option while packaging NuGet artifacts

  # MSBuild verbosity level
  verbosity: normal

# scripts to run before build
before_build:
- nuget restore
- ps: gitversion /l console /output buildserver /updateAssemblyInfo

# scripts to run after build (working directory and environment changes are persisted from the previous steps)
after_build:
  - ps: |
      $ErrorActionPreference = ""Stop"";

      Function Show([string]$arg)
      {{
          Write-host $arg -ForegroundColor blue
      }}

      $loc = Get-Location
      Show $loc

      $FullSemVer = gitversion /output json /showvariable FullSemVer
      Show $FullSemVer

      Get-ChildItem -Path $loc -Filter *.csproj -Recurse -File -Name| ForEach-Object {{
          $csprojFileName = [System.IO.Path]::GetFileName($_)
          $projectRelativeDirectory = [System.IO.Path]::GetDirectoryName($_)
          $csprojRelativePath = [System.IO.Path]::Combine($projectRelativeDirectory, $csprojFileName)

          $packCmd = ""msbuild '$csprojRelativePath' -t:pack -p:version=$FullSemVer -verbosity:minimal""
          Show $packCmd
          Invoke-Expression $packCmd

          Get-ChildItem -Path $projectRelativeDirectory -Filter *.nupkg -Recurse -File -Name| ForEach-Object {{
              $nupkgFileName = [System.IO.Path]::GetFileName($_)
              $nupkgRelativeDirectory = [System.IO.Path]::GetDirectoryName($_)
              $nupkgRelativePath = [System.IO.Path]::Combine($projectRelativeDirectory, $nupkgRelativeDirectory, $nupkgFileName)

              $pushArtifact = ""appveyor PushArtifact '$nupkgRelativePath'""
              Show $pushArtifact
              Invoke-Expression $pushArtifact
          }}
      }}

#---------------------------------#
#       tests configuration       #
#---------------------------------#

# to run tests against only selected assemblies and/or categories
test:
  assemblies:
    only:
      - '**\*.Test.dll'

#---------------------------------#
#     deployment configuration    #
#---------------------------------#

deploy: off

#---------------------------------#
#         notifications           #
#---------------------------------#

notifications:
  # Email
  - provider: Email
    to:
      - '{{{{commitAuthorEmail}}}}'
    on_build_success: true
    on_build_failure: true
    on_build_status_changed: true
";
    }
}