# Spojnice

Cilj igre Spojnice je spojiti nazive iz leve tabele sa odgovarajućim nazivima iz desne tabele.

Igra traje 60 sekundi.

Igrač ima deset pokušaja da spoji odgovarajuće nazive.

Za svaki uspešno spojeni par igrač dobija poen.


## Pokretanje aplikacije

Aplikacija sadrži foldere: gameMicroservice, scoreMicroservice, userMicroservice, View

Za mikroservise potrebno je imati instalirano sledeće na računaru:

●	https://www.microsoft.com/en-us/sql-server/sql-server-downloads

●	https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15

●	https://dotnet.microsoft.com/download/dotnet/5.0


### Pokrenuti SQL Server 

U svakom od mikroservisa u appsettings.json fajlu zameniti string DatabaseConnection za konekciju aplikacije sa bazom podataka 

●	\gameMicroservice\Game\appsettings.json

●	\scoreMicroservice\Score\appsettings.json

●	\userMicroservice\User\appsettings.json

Najednostavniji način da se dodje do “connection string-a” jeste putem visual studija, pratite sledeća upustva:
https://social.msdn.microsoft.com/Forums/sqlserver/en-US/ba6b1757-301c-4545-98e4-69a81b33e876/find-connection-string-used-by-sql-server-management-studio


Putem command prompta ući posebno u svaki od foldera Game, Score i User izvršiti komandu

<pre>dotnet ef database update</pre> 

Ona će na osnovu sadržaja Migrations foldera napraviti strukturu baze. 

Kako ne bi bilo potrebe da se ručno unose navedene komande, napravljen je skript fajl:

-	databaseUpdate.bat koji će u svakom mikroservisu izvršiti komandu dotnet ef database update 

Ako nemate instaliran dotnet-ef instalirajte ga koristeći komandu:

<pre>dotnet tool install --global dotnet-ef</pre>

### Pokretanje mikroservisa

Mikroservisi se pokreću svaki od njih nezavisno od drugog. Otvoriti Command Promt i zatim
preko command promta ići do foldera koji sadrži Program.cs fajl koji je i startni fajl svakog mikroservisa.

●	\gameMicroservice\Game\Program.cs

●	\scoreMicroservice\Score\Program.cs

●	\userMicroservice\User\Program.cs
 
Otvoriti 3 command prompta i preko njih ući u svaki folder od gore navedenih. 
Zatim u sva 3 command prompta kada se nadjemo u gore pomenutim direktorijuma izvršimo komandu

<pre>dotnet run</pre> 

koja će pokrenuti svaki mikroservis.

Dodatno postiji i START.bat fajl koji će ujedno pokrenuti svaki mikroservis kao i angular aplikaciju.

Nakon njihovog pokretanja ne bi bilo lose da se proba da se kontaktira svaki mikroservis preko običnog browsera koji ce biti korišćen za testiranje kako bi odobrili “nesigurnu” konekciju. Mikroservisi su postavljeni da budu https a kako sertifikat nije postavljen od strane reputabilne kompanije browser nas pita da to potvrdimo, svakako testiranje je lokalno tako da je prihvatljivo. Otvoriti browser i otići na sledeće URL adrese i potvrditi https konekciju:

●	https://localhost:5101/swagger/index.html - User mikroservis

●	https://localhost:5201/swagger/index.html - Score mikroservis

●	https://localhost:5301/swagger/index.html - Game mikroservis

Swagger je aktivan tako da je moguće i da se testiraju API putanje nad mikroservisima bez potrebe za klijentskom aplikacijom.

### Pokretanje frontend aplikacije - Angular 

pre pokretanja Angular aplikacije preko command promta uci u direktorijuma game-app.View/Angular/game-app
i izvrsiti 

<pre>npm install</pre>

Ovo ce povuci node module koji su potrebni za pokretanje aplikacije.

Otvoriti još jedan command prompt i ući u folder: \View\Angular\game-app zatim izvršiti komandu 

<pre>npm start</pre>

Koja će pokrenuti komandu ng serve koja pokreće aplikaciju.

### U pretraživaču otvoriti stranicu
<pre>http://localhost:4200</pre>

### Dodatno upustvo

Kada ste pokrenuli aplikaciju potrebno je da kreirate naloge tipa admin kako bi nakon logovanja mogli da pravite nove igre, menjate ih i brišete postojeće.
Admin nalog se pravi na sledeći način

●	Registrovanjem putem REGISTER opcije a zatim Preko Microsoft SQL Server Management Studija promeniti tipa naloga user u tip naloga admin.


Emailovi se šalju putem SMTP protokola. Tako da se može povezati da radi i sa drugim email nalogom. Ukoliko želite da promenite sa kog maila se šalju poruke, potrebno je postaviti varijable u klasama navedenih foldera:

●	\gameMicroservice\Game\Data\UserEmailSender.cs

●	\scoreMicroservice\Score\Data\UserEmailSender.cs

●	\userMicroservice\User\Data\UserEmailSender.cs

## Student koji je uradio projekat

 - [Staša Rašić](https://github.com/Stascha)
