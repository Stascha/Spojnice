import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GameService } from 'src/app/services/game.service';
import { ScoreService } from 'src/app/services/score.service';
import { UserService } from 'src/app/services/user.service';
import { ScoreTabComponent } from './score-tab/score-tab.component';
/** JS function defined in the index.html file. Function that will show the modal to the end user with the text as input. */
declare const sendMessageUserModal: any;

/** Component - GameComponent */
@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
/**
 * This component allows the user to play our game.
 * It also uses to other components to function properly: app-score-tab and app-user-actions-btns
 */
export class GameComponent implements OnInit {
  /** Represents the number of seconds that is shown above our timer while the game is active. */
  timerTxt : string = "Sekunde..."; // prikaz koliko sekundi je ostalo
  /** Represents the precent of how much time has left on the timer. We use it to draw and manipulate with the timer animation. */
  timerHeight : number = 100; // procenat koliko vremena je ostalo, sluzi za istravanje timer-a vizualnog
  /** Number of how many tries has user used while mathing the game pairs. Logic is built in for the 10 tries to be max, when number 10 is reached the game will end. */
  numOfTries : number = 0; // broj pokusaja, kada je 10 igra = zavrsena
  /** Number of correctly mathced game pairs. Number of correct answers by the user. */
  scoreNum : number = 0; // broj tacnih
  /** List that will contains the game data obtained from the server. */
  serverData : any = []; // podaci dobijeni od servera
  /** Contains the currect active game name. */
  currentActiveGameName = "";
  /** We yse ut ti set the selected fields from both columns, makes it easier to check the matching pairs. */
  flagSelectedItems = { // sluzi za cuvanje selectovanih polja iz kolona radi lakse provere
    one: "",
    two: ""
  }
  /** Time interval that will run for our timer, undefined at the beginning */
  interval : any = undefined;

  /**
   * Constructor - reddirects user to the /login if user is not loggedin to our app.
   * @param userService {UserService} to use the loggedin user data
   * @param gameService {GameService} to send and receive data from our microservice API
   * @param router {Router} to change component page
   * @param scoreService {ScoreService} to send and receive data from our microservice API
   * @param scoreTabComponent {ScoreTabComponent}
   */
  constructor(public userService : UserService,
     public gameService : GameService, // kontaktiranje APIja
     private router:Router, // za promenu komponente kao strane
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
   * Used to shuffle the array by random chance.
   * We use this function to shuffle the both columns array that us being displayed to the end user.
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
 * Checks if game has ended, if it did end disable the click event.
 * When we select one item from the column other items that were selected are set as unselected.
 * Set clicked element as selected.
 * Save that it is selected in our flagSelectedItems attribute.
 * @param item {string} Column Game data Item
 * @param pointerFlag {boolean} if flag is true this column answer has not been marked as a correct match answer
 */
  onColumnOneClick(item:string, pointerFlag:boolean){
    if(!pointerFlag){return;}
    //(click)="onColumnOneClick(item.columnOne)" u html fajlu
    // za elemente prve kolone
    if(this.gameService.getGameEndedFlag()){ // ako je igra zavrsena nema korisnik prava da igra na dalje
      return;
    }
    this.serverData.dataOne.forEach((e:any) =>{ 
      // kada se 1 selektuje samo on moze da bude selektovan,
      // stavi svaki da nije selektovan
      e.selectedOne = false;
    })
    let foundItem = this.serverData.dataOne.find((elem:any) => { // nadju onaj koj je kliknut u nasoj listi
      return elem.columnOne === item});
    foundItem.selectedOne = true; // na kliknut elemenat stavi da je selektovan
    this.flagSelectedItems.one = item; // upamti koji je selektovan
  }
/**
 * Check if element from the first column is selected. iF not return noothing which will exist the execution of this method.
 * Set selected element as clicked.
 * Do numOfTries + 1.
 * If elements from first and second column match add one to scoreNum attribute.
 * Set both fields as not selected.
 * Check if number of tries is equal to 10, if it is end the game.
 * @param item {string} Column Game data Item
 */
  onColumnTwoClick(item:string){
    // kada se klikne na elemenat druge kolone
   
    // proveri da li je selektovan neki iz prve kolone
    let foundItem = this.serverData.dataOne.find((elem:any) => {
      return elem.selectedOne === true
    });
    if(!foundItem){return;} // ako nije niko selektovan ne radi nista
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
 * Checks if numOfTries attribute is equal to ten.
 * @returns {boolean}
 */
  isNumberOfTriesEqualToTen():boolean{
    // znaci da igra treba da bude gotova ako je ==10
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
   * When game has ended. Collects the input from the game abd sends the data to our microservice API via gameService.
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
   * Method that will initialize all the parameters when new game starts.
   * It is going to reset numOfTries and scoreNum to zero.
   * Set game as active.
   * Grab new random game data from our microservise API via GameService.
   * Shuffle array for both columns.
   * Set the new timer to run.
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
      //let timerInMinutes = 2;
      //let timerInSeconds = timerInMinutes * 60000;
      let timerInSeconds = 60000;
      let howManySeconds = timerInSeconds/1000;
      let decreasePerMin = 100/howManySeconds;
      this.timerTxt = `${howManySeconds}`; // timerTxt se prikazuje u .html fajlu
      this.interval = setInterval(() => { // stavljamo interval da ide na svakih 1s da se izvri funkcija unutar
        if(this.gameService.getGameEndedFlag()){clearInterval(this.interval);} // ako je igra zavrsena prestaje sa intervalom
        howManySeconds--; // umanji sekunde za 1s
        this.timerTxt = `${howManySeconds}`;// timerTxt se prikazuje u .html fajlu
        this.timerHeight -= decreasePerMin; // da smanji % prikaza timera graficki
        if (this.timerHeight <= 0) { // ako je % 0 znaci da je gorova igra
            clearInterval(this.interval); // prekini interval
            this.gameEndedSendScoreResult(); // prekini igru
        }
    }, 1000);
  });
  }
}
