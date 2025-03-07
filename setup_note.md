```cmd
dotnet new create wpf -f "net8.0"
```

- create `appsettings.json`

- insert into `.csproj`

```xml
  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.13">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.SQLServer" Version="8.0.13" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="8.0.1" />
  </ItemGroup>

  <ItemGroup>
    <None Update="appsettings.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
  </ItemGroup>
```

```cmd
dotnet build
dotnet run
```

- `dotnet-ef`

```
dotnet ef dbcontext scaffold "Server=ACER\SQLEXPRESS;uid=sa;password=123456;database=PRN212_project;Encrypt=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SQLServer --output-dir Models --namespace ""
```

- modify `context.cs`

- insert `using`

```cs
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
```

```cs
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        var configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
    }
```
