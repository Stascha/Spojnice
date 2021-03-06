# Spojnice

<br />

Cilj igre Spojnice je spojiti nazive iz leve tabele sa odgovarajućim nazivima iz desne tabele.<br />
Igra traje 60 sekundi. <br /> 
Igrač ima deset pokušaja da spoji odgovarajuće nazive.<br />
Za svaki uspešno spojeni par igrač dobija poen.<br />

<br /> <br /> 
## Pokretanje aplikacije 
<br />

Aplikacija ima sledeće foldere: gameMicroservice, scoreMicroservice, userMicroservice i View

Za mikroservise potrebno je imati instalirano sledeće na računaru: <br />
●	https://www.microsoft.com/en-us/sql-server/sql-server-downloads <br />
●	https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15 <br />
●	https://dotnet.microsoft.com/download/dotnet/5.0 


Pokrenuti SQL Server 
U svakom od mikroservisa zameniti string za konekciju - DatabaseConnection <br />
●	\gameMicroservice\Game\appsettings.json <br />
●	\scoreMicroservice\Score\appsettings.json <br />
●	\userMicroservice\User\appsettings.json <br /> <br />
Najednostavniji način da se dodje do “connection string-a” jeste putem visual studija, pratite sledeća upustva:
https://social.msdn.microsoft.com/Forums/sqlserver/en-US/ba6b1757-301c-4545-98e4-69a81b33e876/find-connection-string-used-by-sql-server-management-studio

Ući u svaki od foldera Game, Score i User i pojedinačno putem command prompta izvršiti komandu<br /> `dotnet ef database update` <br /> ona će pokupiti sta se nalazi u Migrations folderu i napraviti strukturu baze. <br /> Ako nemate instaliran dotnet-ef, instalirajte ga koristeći komandu :<br /> `dotnet tool install --global dotnet-ef` 

Kako ne bi bilo potrebe ručno da se unose komande, napravljen je skriptni fajl <br />
● databaseUpdate.bat <br /> koji će izvršiti dotnet ef database update komandu u svakom mikroservisu.

Mikroservisi se pokreću svaki od njih nezavisno od drugog. <br /> 
Otvoriti Command Promt i zatim preko command promta ići do foldera koji sadrži Program.cs fajl koji je i startni fajl svakog mikroservisa. <br />
●	\gameMicroservice\Game <br />
●	\scoreMicroservice\Score <br />
●	\userMicroservice\User
 
 
Treba otvoriti 3 command prompta i preko njih ući u svaki od gore navedenih foldera. <br />Zatim u
sva 3 command prompta treba se pozicionirati u gore pomenutim direktorijumima i uraditi <br /> `dotnet run` <br />koja će pokrenuti svaki mikroservis.

Dodatno postiji i START.bat fajl koji će ujedno pokrenuti svaki mikroservis kao i angular aplikaciju.

Nakon njihovog pokretanja može da se kontaktira svaki preko običnog browsera koji će biti korišćen za testiranje kako bi odobrili “nesigurnu” konekciju. Mikroservisi su postavljeni da budu https a kako sertifikat nije postavljen od strane reputabilne kompanije browser nas pita da to potvrdimo, svakako testiranje je lokalno tako da je prihvatljivo. <br /> Otvoriti browser i otići na sledeće URL adrese i potvrditi https konekciju:

●	https://localhost:5101/swagger/index.html - User mikroservis <br />
●	https://localhost:5201/swagger/index.html - Score mikroservis <br />
●	https://localhost:5301/swagger/index.html - Game mikroservis

Swagger je aktivan tako da je moguće i da se testiraju API putanje nad mikroservisima bez potrebe za klijentskom aplikacijom.

Pokretanje frontend aplikacije - Angular <br />
Otvoriti još jedan command prompt i ući u folder \View\Angular\game-app zatim uraditi <br />`ng run`<br />
Pored ng run komande isto je moguće uraditi i sam build i nakon toga može aplikacija sama preko generisanih html, css i js fajlova da se pokrene bez potrebe za pokretanjem angular “servera”.

Ukoliko je angular pokrenut preko ng serve komande onda će angular klijentska aplikacija biti dostupna na adresi <br />`localhost:4200`
 <br />
 <br />
## Dodatno upustvo

Kada uradite navedena uputstva verovatno je da nemate kreiran ni jedan nalog. <br />Potrebno je da se napravi nalog tipa admin kako bi nakon logovanja mogli da pravite nove igre. <br />Admin nalog može da se napravi preko Microsoft SQL Server Management Studija.


Slanje emailova je uvezano putem SMTP protokola. Tako da možete povezati da radi i sa drugim mail nalozima. Ukoliko želite da promenite sa kog maila se šalju poruke, potrebno je postaviti varijable u klasama navedenih foldera: <br />

●	\gameMicroservice\Game\Data\UserEmailSender.cs <br />
●	\scoreMicroservice\Score\Data\UserEmailSender.cs <br />
●	\userMicroservice\User\Data\UserEmailSender.cs <br />

<br /> <br />
## Korišćeni resursi 
<br /> 
Slanja emailova <br />
https://docs.microsoft.com/en-us/dotnet/api/system.net.mail?view=net-5.0  
<br /> <br />
Kriptovanja passworda <br />
https://github.com/BcryptNet/bcrypt.net
