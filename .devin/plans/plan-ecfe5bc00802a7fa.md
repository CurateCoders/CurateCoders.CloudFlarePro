---
agent: devin-local
session: merciful-drip
created: 2026-10-23T13:38:36Z
---
# CloudFlarePro multi-major Umbraco release plan (v14-v18)

Release one NuGet package per Umbraco major (14.0.0, 15.0.0, 16.0.0, 17.0.0, 18.0.0) from the existing release/* CI pipeline, each with the right TFM and Umbraco dependency floor.

## Findings
- Repo: single project `src/CurateCoders.CloudFlarePro` (net8.0, refs `Umbraco.Cms.Web.Common` pinned 13.13.0). Code = `CloudImageHtmlHelper.cs` + models + example views. No tests.
- Release flow: push a branch named `release/<version>` -> `.github/workflows/dotnet.yml` (windows-2022, .NET 8 only) builds with `-p:Version=<branch suffix>` and does `dotnet nuget push` using `NUGET_API_KEY` secret. `develop` is default branch; `main` exists. `build/Push.ps1` is legacy, unused by CI.
- History so far: one release branch per Umbraco minor (13.9.2 ... 13.13.0), merged via PR into develop. NuGet currently has up to 13.13.0 (plus old 0.x).
- Umbraco.Cms.Web.Common on NuGet: 14.0.0-14.3.4, 15.0.0-15.4.4, 16.0.0-16.5.1, 17.0.0-17.8.0, 18.0.0-18.3.0-rc exist. Local SDKs: 8, 9, 10 installed.
- Risk: only 13.x tested. v14+ may break: `Umbraco.Extensions` `ToQueryString()` on `RouteValueDictionary` (legacy path), `Microsoft.CodeAnalysis.CSharp` stray using, razor view content files.

## Decisions (confirmed)
- One release per major, version `X.0.0`; patch (`X.0.1`) only for our own fixes.
- Single codebase; only csproj changes per major; cut a `release/X.0.0` branch from develop for each.
- Dependency range `[X.0.0,(X+1).0.0)` so it installs on any minor of that major.
- 13.x: leave 13.13.0 as final (optionally publish 13.0.0 is NOT needed; 13.13.0 already satisfies major-13).

## Steps
1. Create `feature/multi-major` from develop (merge release/13.13.0 first so develop has the latest).
2. Make csproj version-driven: `<TargetFramework>` and Umbraco version conditioned on an `UmbracoMajor` property (default 17), e.g. net8.0 for 13/14, net9.0 for 15/16, net10.0 for 17/18 (verify each major's TFM from the Umbraco package nuspec before finalising). Reference `Umbraco.Cms.Web.Common` Version="[$(UmbracoMajor).0.0,$(NextMajor).0.0)". Pass `-p:UmbracoMajor=X` in CI.
3. Update CI workflow: add .NET 8/9/10 SDKs via setup-dotnet, derive `UmbracoMajor` from branch version (first segment), upgrade checkout/setup-dotnet actions, add `--skip-duplicate` to push so reruns don't fail. Push only `.nupkg`/`.snupkg` for that version (clean bin first) to avoid re-pushing old packages.
4. Locally build + pack each major (14-18) with `dotnet pack -c Release -p:Version=X.0.0 -p:UmbracoMajor=X`; fix compile breaks with minimal conditional code (`#if`) or replace the `ToQueryString()` usage with own implementation (removes Umbraco-version coupling). Remove unused `using Microsoft.CodeAnalysis.CSharp`.
5. Smoke test: inspect nupkg nuspec (dependency range, TFM), and ideally render the helper in a throwaway Umbraco site for 14, 17 and 18.
6. Update README (supported versions table, versioning policy, install command per major) and bump description/copyright if needed.
7. Merge to develop via PR. Then cut and push, in order, `release/14.0.0`, `release/15.0.0`, `release/16.0.0`, `release/17.0.0`, `release/18.0.0` (each from the same develop commit). Each push triggers CI publish. Verify each on nuget.org (snupkg too), then tag `v14.0.0`... in git.
8. Future: when a new major appears, add branch `release/N.0.0`; for fixes, bump patch on all supported majors.

## Verification
- `dotnet build`/`pack` for each major locally; inspect nuspec.
- GitHub Actions green per release branch; packages visible on nuget.org.
- Install into a test Umbraco 14/17/18 site and render `GetCloudflareImageTag`.

## Risks
- NuGet publishes are irreversible (can only unlist), so dry-run packs locally first and push one major (e.g. 17.0.0) before the rest.
- Umbraco 14+ API/TFM differences untested; v18 is new so check it's stable (18.2.1 is latest stable).
- Needs `NUGET_API_KEY` secret valid; I will not push without your confirmation.
