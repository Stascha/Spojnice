import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GameService } from 'src/app/services/game.service';
import { ScoreService } from 'src/app/services/score.service';
import { UserService } from 'src/app/services/user.service';
import { ScoreTabComponent } from './score-tab/score-tab.component';
/** JS funkcija definisana u index.html file. Funkcija koja prikazuje modal korisniku - igracu sa tekstom. */
declare const sendMessageUserModal: any;

/** Component - GameComponent */
@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
/**
 * Komponenta koja omogucava igracu da igra igru.
 * Takodje koristi i druge komponente : app-score-tab i app-user-actions-btns
 */
export class GameComponent implements OnInit {
  /** Predstavlja broj preostalih sekundi koje su prikazane iznad tajmera dok je igra aktivna */
  timerTxt : string = "Sekunde..."; 
  /** Predstavllja procenat koliko vremena je ostalo, sluzi za iscrtavanje vizualnog timer-a*/
    timerHeight: number = 100; 
  /** Broj pokušaja pogadjanja odgovarajucuih parova koje je igrac iskoristio.
   * Posle 10 pokušaja igra se zavrsava. */
  numOfTries : number = 0; 
  /** Broj tacno pogodjenih parova. */
  scoreNum : number = 0;
  /**Lista koja sadrzi podatke o igri dobijene od servera. */
  serverData : any = []; 
  /** Sadrzi naziv trenutno aktivne igre. */
  currentActiveGameName = "";
  /** Sluzi za cuvanje selectovanih polja iz obe kolone radi lakse provere */
  flagSelectedItems = {
    one: "",
    two: ""
  }
  /** Vremenski interval za tajmer, nedefinisan na pocetku */
  interval : any = undefined;

  /**
   * Konstruktor - redirektuje korisnika - igraca na stranicu za logovanje ako korisnik - igrac nije ulogovan.
   * @param userService {UserService} za koriscenje podataka ulogovanog igraca
   * @param gameService {GameService} za slanje i dobijanje podataka od microservice API
   * @param router {Router} za promenu stranice 
   * @param scoreService {ScoreService} za slanje i dobijanje podataka od microservice API
   * @param scoreTabComponent {ScoreTabComponent}
   */
  constructor(public userService : UserService,
     public gameService : GameService, // kontaktiranje APIja
     private router:Router, // za promenu strane
     private scoreService: ScoreService, // kontaktira API
     private scoreTabComponent : ScoreTabComponent) {
    if(userService.getLoggedinUser().id === -1){
      // ako nije logovan radi reddirect na /login stranu
      // -1 smo stavili kao defaultni i za logout kada nema user-a logovanog
      this.router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
      this.router.navigate(["/login"]));
    }
    this.serverData = [];
  }
  /** Empty ngOnInit */
  ngOnInit(): void {
   
  
  } //ngOnInit end;
  /**
   * Funkcija na slucajan nacin premesta elemente nizaova za obadve kolone koje se prikazuju igracima tokom igre.
  */
  shuffleArray(array:Array<any>) { // da rasporedi random elemente liste
    for (var i = array.length - 1; i > 0; i--) {
        var j = Math.floor(Math.random() * (i + 1));         
        var temp = array[i]; 
        array[i] = array[j]; 
        array[j] = temp; 
    }
    return array;
}

/**
 * Proverava da li je igra zavrsena, ako je igra zavrsena onemogucava da se dalje selektuju elementi kolona.
 * Kada se selektuje element kolone, element koji je vec bio selektovan se postavi da ne bude selektovan.
 * Prikazuje da je element kolone na koji je kliknuto selektovan.
 * Sacuva da je selektovan u flagSelectedItems atributu.
 * @param item {string} Column Game data Item
 * @param pointerFlag {boolean} if flag is true this column answer has not been marked as a correct match answer
 */
  onColumnOneClick(item:string, pointerFlag:boolean){
    if(!pointerFlag){return;}
    //(click)="onColumnOneClick(item.columnOne)" u html fajlu
    // za elemente prve kolone
    if(this.gameService.getGameEndedFlag()){ // ako je igra zavrsena nema korisnik prava da igra dalje
      return;
    }
    this.serverData.dataOne.forEach((e:any) =>{ 
      // kada se jedan element  selektuje samo on moze da bude selektovan,
      // stavi svaki drugi da nije selektovan
      e.selectedOne = false;
    })
    let foundItem = this.serverData.dataOne.find((elem:any) => { // nadju onaj koj je kliknut u nasoj listi
      return elem.columnOne === item});
    foundItem.selectedOne = true; // na kliknut elemenat stavi da je selektovan
    this.flagSelectedItems.one = item; // upamti koji je selektovan
  }
/**
 * Proverava da li je element iz prve kolone selektovan. 
 * Postavlja selektovan element da je selektovan.
 * Povecava broj pokusaja za jedan numOfTries + 1.
 * Ako su selektovani elementi iz prve i druge kolone odgovarajuci povecava za jedan scoreNum atribut.
 * Set both fields as not selected.
 * Proverava da li je broj pokušaja 10, ako je broj pokusaja 10 igra se zavrsava.
 * @param item {string} Column Game data Item
 */
  onColumnTwoClick(item:string){
    // kada se klikne na elemenat druge kolone
    // proveri da li je selektovan neki iz prve kolone
    let foundItem = this.serverData.dataOne.find((elem:any) => {
      return elem.selectedOne === true
    });
    if(!foundItem){return;} // ako nije selektovan ni jedan element iz prve kolone ne radi nista
    // korisnik mora da izabere iz prve kolone zatim iz druge
    this.serverData.dataTwo.forEach((e:any) =>{e.selectedTwo = false;})// stavi svaki da nije selektovan
    foundItem = this.serverData.dataTwo.find((elem:any) => { 
      // nadji kliknut elemenat i selektuj ga
      return elem.columnTwo === item});
    if(foundItem.success){return;} // ako je polje markirano da je tacno izabrano, nezelimo da povecavamo broj pokusaja i da idemo dalje
    foundItem.selectedTwo = true;
    this.numOfTries++;
    this.flagSelectedItems.two = item; // zapamti koji je selektovan
    let isSuccess = this.serverData.data.find((elem:any) => {
      // prodji kroz server listu i upredi ako postoji par izmendju selektovanih u prvoj i drugoj koloni
      return elem.columnOne === this.flagSelectedItems.one && elem.columnTwo === this.flagSelectedItems.two
    });
    if(isSuccess){ // ako je tacan par
      foundItem.success = true; // stavi ih da je par pronadjen
      foundItem.pointer = false; // zaustavi da korisnik moze da klikce 
      this.scoreNum++; // povecaj SCORE
    }
    this.serverData.dataTwo.forEach((e:any)=>{
      e.selectedOne = false;
      e.selectedTwo = false;
    }) // postavi svaki da nije selektovan
    if(this.isNumberOfTriesEqualToTen()){
      this.gameEndedSendScoreResult();
    }
  }
/**
 * Proverava da li numOfTries atribut jednak 10.
 * @returns {boolean}
 */
  isNumberOfTriesEqualToTen():boolean{
    // Igra treba da bude gotova ako je numOfTries === 10
    if(this.numOfTries === 10){
      return true;
    } return false;
  }
  /** Disasble pointer cursor - cursor change to pointer on hover on all column elements */
  disableClickOnItems():void{
    // zaustavi klik animaciju
    this.serverData.data.forEach((e:any)=>{
      e.pointer = false;
    });
  }
  /**
   * Kada se igra zavrsi. Salje skor dobijen iz igre u microservice API putem gameService.
   */
  gameEndedSendScoreResult(){
    this.gameService.gameActive = true;
    this.gameService.setGameEndedFlag(true); // igra je gotova
    sendMessageUserModal("Igra je zavrsena!")
    this.disableClickOnItems();
      this.scoreService.pushScore( // dodaj score na server
        // server radi proveru i cuva samo najbolji score od zadatog user-a
      this.scoreNum,
      this.userService.getLoggedinUser().username
    ).subscribe((data:any)=>{
      console.log(data)
      this.scoreTabComponent.updateScoreTableView();
    });
  }

  /**
   * Metod koji inicijalizuje sve parametre kada nova igra pocne.
   * Postavlja vrednosti numOfTries i scoreNum na 0.
   * Postavlja igru da je aktivna.
   * Uzima novu slucjno izabranu igru tj njene podatke iz microservise API via GameService.
   * Ispremesta elemente oba niza, koji se prikazuju kao elementi kolona, na slucajan nacin.
   * Startuje novi tajmer.
   */
  startTheGame(){
    this.gameService.setGameEndedFlag(false);
    this.timerHeight = 100; // %
    try{clearInterval(this.interval);}catch{};

    this.numOfTries= 0; 
    this.scoreNum = 0; 

    // vucemo podatke sa servera
    // random radimo za 2 nove liste kako bi redosled u obe kolone bio drugaciji, svaki prikaz kolone je prikaz iz druge liste
    this.gameService.getGame().subscribe((data)=>{
      this.currentActiveGameName = data.name;
      this.serverData.data = JSON.parse(data.data);
        this.serverData.data.forEach((e: any) => {
          e.pointer = true;
        // da moze da se klikne na elemenat
        // [ngClass] u .html fajlu postavlja CSS imena u zavisnosti od true/false boolean varijabli
      })
      this.serverData.dataOne = this.shuffleArray(this.serverData.data);
      this.serverData.dataTwo=[]
      Object.keys(this.serverData.dataOne).forEach(key=>this.serverData.dataTwo[key]=this.serverData.dataOne[key]);
      this.serverData.dataTwo = this.shuffleArray(this.serverData.dataTwo);
    
      //postavljamo nas timer ispod.....
      //let timerInMinutes = 1;
      //let timerInSeconds = timerInMinutes * 60000;
      let timerInSeconds = 60000;
      let howManySeconds = timerInSeconds/1000;
      let decreasePerMin = 100/howManySeconds;
      this.timerTxt = `${howManySeconds}`; // timerTxt se prikazuje u .html fajlu
      this.interval = setInterval(() => { // stavljamo interval da ide na svakih 1s da se izvrsi funkcija unutar
        if(this.gameService.getGameEndedFlag()){clearInterval(this.interval);} // ako je igra zavrsena prestaje sa intervalom
        howManySeconds--; // umanji sekunde za 1s
        this.timerTxt = `${howManySeconds}`;// timerTxt se prikazuje u .html fajlu
        this.timerHeight -= decreasePerMin; // da smanji % prikaza timera graficki
        if (this.timerHeight <= 0) { // ako je % 0 znaci da je gotova igra
            clearInterval(this.interval); // prekini interval
            this.gameEndedSendScoreResult(); // prekini igru
        }
    }, 1000);
  });
  }
}
