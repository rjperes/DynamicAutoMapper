# DynamicAutoMapper

Auto mapping for AutoMapper

## Publishing NuGet Package

This repository includes a GitHub Actions workflow that automatically publishes the NuGet package when a new version tag is pushed.

### How to Publish

#### Option 1: Using Version Tags (Recommended)

1. Ensure your code is ready for release
2. Create and push a version tag:
   ```bash
   git tag v1.0.0
   git push origin v1.0.0
   ```
3. The GitHub Actions workflow will automatically:
   - Build the project in Release configuration
   - Pack the NuGet package with the version from the tag
   - Publish to NuGet.org

#### Option 2: Manual Trigger

1. Go to the Actions tab in the repository
2. Select "Publish NuGet Package" workflow
3. Click "Run workflow"
4. Enter the version number (e.g., 1.0.0)
5. Click "Run workflow" button

### Prerequisites

- A `NUGET_API_KEY` secret must be configured in the repository settings
  - Go to repository Settings → Secrets and variables → Actions
  - Add a new secret named `NUGET_API_KEY` with your NuGet.org API key

### Version Tags

The workflow is triggered by tags matching the pattern `v*.*.*` (e.g., `v1.0.0`, `v2.1.3`).