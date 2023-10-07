# CPDatabase (W.I.P.)
Source code for the upcoming Classic Patch 14 (https://fifaclassicpatch.altervista.org) database website. Uses 4.1 patch version + Jbou41 addon v3.1 + Valambrosio addon v1.1.

21/09/2023: beta 2.

Made with .NET 5 Core + Entity Framework 5 Core + MS SQL Server 2016.

## Preparing to work
1. Download/clone the repository.
2. Download the images archive (http://www.mediafire.com/file/ujwnd93j4lmnvj0) and unpack it anywhere you wish.
3. Clone the appsettings.template.json file and change the "template" to your environment name within the copy name (e.g. appsettings.Local.json).
4. Uncomment the file from the previous step and change the line 7 according to the previous step.
5. Deploy (restore) the database in your local MS SQL Server using the ClassicPatchDatabase.bak file.
5. Change the line 5 in the copied file from the step 3 according to the previous step.
6. Change the line 22 in the efpt.config.json file according to the step 4.