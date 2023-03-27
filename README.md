# CPDatabase (W.I.P.)
Source code for the upcoming Classic Patch 14 (https://fifaclassicpatch.altervista.org) database website. Uses 4.1 patch version + Jbou41 addon v3.1 + Valambrosio addon v1.1.

27/03/2022: beta 1.

Made with .NET 5 Core + Entity Framework 5 Core + MS SQL Server 2016.

## Preparing to work
1. Download/clone the repository.
2. Download the images archive (https://www.mediafire.com/file/xva2pzq9ecnnak0) and unpack it anywhere you wish.
3. Change the line 78 in the Startup.cs file according to the previous step.
4. Deploy (restore) the database in your local MS SQL Server using the ClassicPatchDatabase.bak file.
5. Change the line 3 in the appsettings.json file according to the previous step.
6. Change the line 22 in the efpt.config.json file according to the step 4.
