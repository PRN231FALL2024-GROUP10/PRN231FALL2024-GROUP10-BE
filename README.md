# Project Installation Instructions

## Overview

This README provides instructions for all team members on how to install the necessary NuGet packages for the `JobScial` solution. Each project within the solution has specific package requirements. Please follow the instructions carefully to ensure that all dependencies are correctly installed.

## Prerequisites

- Ensure that you have [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine (version 7.0).
- A terminal or command prompt with access to the project's directories.

## Package Installation

### 1. JobScial.WebAPI

Navigate to the `JobScial.WebAPI` project directory and run the following commands to install the required packages:

```bash
cd JobScial.WebAPI

dotnet add package Microsoft.AspNetCore.OData --version 8.0.12
dotnet add package Microsoft.AspNetCore.OpenApi --version 7.0.10
dotnet add package Swashbuckle.AspNetCore --version 6.5.0
```

### 2. JobScial.BAL

Navigate to the `JobScial.BAL` project directory and run the following commands:

```bash
cd JobScial.BAL

dotnet add package FluentValidation --version 11.10.0
dotnet add package Microsoft.AspNetCore.Http --version 2.1.34
dotnet add package Newtonsoft.Json --version 13.0.3
dotnet add package System.IdentityModel.Tokens.Jwt --version 8.1.0
```

### 3. JobScial.DAL

Navigate to the `JobScial.DAL` project directory and run the following commands:

```bash
cd JobScial.DAL

dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.20
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0.20
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 7.0.20
dotnet add package Microsoft.Extensions.Configuration --version 7.0.0
dotnet add package Microsoft.Extensions.Configuration.Json --version 7.0.0
```

## Conclusion

After running the above commands in the respective project directories, all necessary packages will be installed with the specified versions. If you encounter any issues during the installation process, please reach out for assistance.

Thank you for your collaboration!
