# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Changed
- Target frameworks: net6.0 t/m net10.0; MVC en Http komen uit het gedeelde framework (FrameworkReference Microsoft.AspNetCore.App) in plaats van de ASP.NET Core 2.x-packages.
- CodeCube.Core 4.0.8, System.ServiceModel.Syndication 10.0.14.
- Package-metadata gecentraliseerd in Directory.Build.props (MIT-licentie, PackageIcon, repository-URL).

### Removed
- Legacy .NET Framework-project CodeCube.Mvc (package is unlisted op nuget.org) en de oude .nuspec-bestanden.

## [2.0.1] - t.b.d

### Added
- Migrated filters from old Borlo library.

### Changed
- Renamed HTML-helpers.
- Added support for .NET Core 2.x and 3.x.
- Updated referenced packages.

### Removed
- Constant with Twitter URL.
- HTML-Helper for rendering Google Analytics code.
- Moved ExpressionConverter to CodeCube.Core.
- Moved PredicateBuilder to CodeCube.Core.
- Moved OutputFilter to CodeCube.Core.
- Moved ETextboxType to CodeCube.Core.
- Moved BitmapExtensions to CodeCube.Core.
- Moved ByteArrayExtensions to CodeCube.Core.
- Moved DateTimeExtensions to CodeCube.Core.
- Moved ENumExtensions to CodeCube.Core.
- Moved method GetImageDimensions to CodeCube.Core as StreamExtension.
- Removed HasFile method.
- Moved StringExtensions to CodeCube.Core
- Moved ImageFunctions to CodeCube.Core
- 