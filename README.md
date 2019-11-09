# Ivory Tower
A dashboard project to prevent the ivory tower position of us architects 😅

## Preview
![Preview](./preview.png)

## Setup
1. Copy `Web/appsettings.json` to `Web/appsettings.Development.json`
2. Add database connection:
```json
"ConnectionStrings": {
"DefaultConnection": "Server=127.0.0.1;Database=IvoryTower;Uid=sa;Pwd=yourStrong(!)Password;"
},
```
3. migrate the database: `dotnet ef database update -s Web`