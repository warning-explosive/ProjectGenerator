namespace SpaceEngineers.ProjectGenerator
{
    using Core.CompositionRoot.Attributes;
    using Core.CompositionRoot.Enumerations;
      
    [Lifestyle(EnLifestyle.Singleton)]
    internal class GitVersionGenerator : SolutionConfigurationFileGeneratorBase
    {
        protected override string FileName => "GitVersion.yml";

        protected override string Content(SolutionInformation solutionInfo) =>
@"assembly-versioning-scheme: MajorMinorPatch
assembly-file-versioning-scheme: MajorMinorPatchTag
assembly-informational-format: '{SemVer}'
mode: ContinuousDelivery
tag-prefix: ''
continuous-delivery-fallback-tag: '{BranchName}'
next-version: 1.0
major-version-bump-message: '\[semver:\s?(breaking|major)\]'
minor-version-bump-message: '\[semver:\s?(feature|minor)\]'
patch-version-bump-message: '\[semver:\s?(fix|patch)\]'
no-bump-message: '\[semver:\s?(none|skip)\]'
legacy-semver-padding: 5
build-metadata-padding: 5
commits-since-version-source-padding: 5
commit-message-incrementing: Enabled
branches:
  master:
    mode: ContinuousDelivery
    tag: ''
    increment: Patch
    prevent-increment-of-merged-branch-version: true
    track-merge-target: false
    regex: master
    source-branches: []
    is-source-branch-for:
    - develop
    - hotfix
    - support
    tracks-release-branches: false
    is-release-branch: false
  develop:
    mode: ContinuousDeployment
    tag: alpha
    increment: Minor
    prevent-increment-of-merged-branch-version: false
    track-merge-target: true
    regex: develop
    source-branches:
    - master
    is-source-branch-for:
    - feature
    - release
    tracks-release-branches: true
    is-release-branch: false
  release:
    mode: ContinuousDelivery
    tag: beta
    increment: Patch
    prevent-increment-of-merged-branch-version: true
    track-merge-target: false
    regex: release-(\d)+.(\d)+.(\d)+
    source-branches:
    - develop
    is-source-branch-for: []
    tracks-release-branches: false
    is-release-branch: true
  feature:
    mode: ContinuousDelivery
    tag: alpha
    increment: Inherit
    prevent-increment-of-merged-branch-version: false
    track-merge-target: false
    regex: feature-(\d)+
    source-branches:
    - develop
    is-source-branch-for: []
    tracks-release-branches: false
    is-release-branch: false
  pull-request:
    mode: ContinuousDelivery
    tag: PullRequest
    increment: Inherit
    prevent-increment-of-merged-branch-version: false
    tag-number-pattern: '[/-](?<number>\d+)[-/]'
    track-merge-target: false
    regex: pull-request-(\d)+
    source-branches:
    - develop
    is-source-branch-for: []
    tracks-release-branches: false
    is-release-branch: false
  hotfix:
    mode: ContinuousDelivery
    tag: beta
    increment: Patch
    prevent-increment-of-merged-branch-version: false
    track-merge-target: false
    regex: hotfix-(\d)+.(\d)+.(\d)+
    source-branches:
    - master
    - support
    is-source-branch-for: []
    tracks-release-branches: false
    is-release-branch: false
  support:
    mode: ContinuousDelivery
    tag: ''
    increment: Patch
    prevent-increment-of-merged-branch-version: true
    track-merge-target: false
    regex: support-(\d)+.(\d)+.(\d)+
    source-branches:
    - master
    is-source-branch-for:
    - hotfix
    tracks-release-branches: false
    is-release-branch: false
ignore:
  sha: []
increment: Inherit
commit-date-format: yyyy-MM-dd
";
    }
}