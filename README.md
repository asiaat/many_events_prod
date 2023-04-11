# Külaliste Registreerimissüsteem

Eeltingimused
 ---------------------------
 
 - Visual Studio 2022
 - git
 
 1. Kood
 
 Kood tuleb lahtipakkida või kloonida repositooriumist endale soovitud folderisse.
 Seejärel minna sinna folderisse ning avada VS 2022 fail ManyEvents.sln
 
 2. Andmebaasi initisaliseerimine
 
Olla/minna projekti kataloogi , sealt käivitada käsk:

	 dotnet ef database update 
 
	 Build started...
	 Build succeeded.
	 Done.
   
3. Andmebaasi vaikeväärtustega täitmiseks käivitada  
	
		 dotnet run /seed 
	 
   
4. Logid

   Süsteemi kirjutab logisid faili ManyEvents-20230411.log	
   
5. Käivitus

Käivitamiseks valida start nupp.

Selle käigus toimub buildimine ja ka andmebaasi  migratsioon


6. Testimine

Testskriptide jooksutamiseks käivitada projekti kataloogis käsk

	  dotnet test

	 Starting test execution, please wait...
	A total of 1 test files matched the specified pattern.

	Passed!  - Failed:     0, Passed:     4, Skipped:     0, Total:     4, Duration: 1 ms - Testing.dll (net7.0)


 

