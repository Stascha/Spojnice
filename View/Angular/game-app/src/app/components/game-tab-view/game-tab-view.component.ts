import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Game, GameService } from 'src/app/services/game.service';
import { UserService } from 'src/app/services/user.service';
/** JS function defined in the index.html file. Function that will initialize modal reference when called. */
declare const initializeUpdateGameModalVar: any;
/** JS function defined in the index.html file. Function that will show the modal to the end user. */
declare const updateGameModalShow: any;
/** JS function defined in the index.html file. Function that will show the modal to the end user with the text as input. */
declare const sendMessageUserModal: any;
/**
 * Component - GameTabViewComponent
 */
@Component({
  selector: 'app-game-tab-view',
  templateUrl: './game-tab-view.component.html',
  styleUrls: ['./game-tab-view.component.css']
})
/**
 * GameTabViewComponent - page that enables the admin to edit, delete and create new components
 */
export class GameTabViewComponent implements OnInit {
  /**Contains game table data to list as well as data about the data fields which are hidden at the beginning.*/
  private gameTableList : Array<any> = [];
  /** Contains an array of Input fields eiither for updating the Game fields, or for creating new Game data fields. */
  public inputGameData : Array<any> = [];
  /** Contains game input name for updating or when creating new Game */
  public inputNameGameData : string = "";
  /**Contains if all fields are valid in our Input Dialog: when creating or updating our game. */
  public formInputInvalid : boolean = false;
  /** when we want to update the Game Data, this field will contain ID of the GAME in our database. */
  private inputIDClicked: number = -1;
  /** if Input Dialog is active : either for create or update Game operation. */
  private isCreateModalActive : boolean = false;
  /**
   * Constructor : if user is not admin, user will be reddirected to the /login component.
   * @param userService {UserService}
   * @param router {Router}
   * @param gameService {GameService}
   */
  constructor(
    private userService : UserService,
    private router: Router,
    private gameService : GameService,
  ) {
    if(userService.getLoggedinUser().role != "admin"){
      // korisnik mora da bude admin kako bi pristupio strani!
      this.router.navigateByUrl('/', {skipLocationChange: true}).then(()=> 
      this.router.navigate(["/login"])); 
      console.log("not loggedin!")
    }
    this.initEmptyInputUpdateForm(); 
    this.gameService.gameActive = false;
   }

    /** Empties out Input dialog form */
   initEmptyInputUpdateForm(){
     // postavlja sva polja za izmene na prazne stringove
    this.inputGameData = [];
    for(let i = 0; i < 10;i++){
      this.inputGameData.push({columnOne: "", columnTwo: ""});
    }
   }

   /** This method will use gameService to get the data related to all the games in order to list them and show to the end user.*/
   initMainDataTable(){
     //dovlaci podatke iz baze, kontaktira API
    this.gameService.getAllGames().subscribe((e)=>{
      this.gameTableList = e;
      console.log(e)
    },(err)=>{
      console.log(err)
      sendMessageUserModal("Doslo je do greske pri ucitavanju podataka...")
    })
   }
  /**
   * sets gameEndedFlag attribute of GameService class to truue.
   * calls our local class method initMainDataTable.
   */
  ngOnInit(): void {
    this.gameService.setGameEndedFlag(true);
    this.initMainDataTable();
    
    initializeUpdateGameModalVar();
  }
  /**
   * When user chooses and option to update the game this method is called.
   * 
   */
  onUpdateGameFormSubmit():void{
    if(!this.checkIfFieldsNotEmpty()){
      this.formInputInvalid = true;
      return;
    } else{ this.formInputInvalid = false;}

    let item = {
      id: this.inputIDClicked,
      name: this.inputNameGameData,
      data: JSON.stringify(this.inputGameData)
    }

    this.gameService.updateGame(item).subscribe((e)=>{
      this.onUpdatGameModalClose();
      sendMessageUserModal(`igra je promenjena ${item.id}`);
      this.initMainDataTable();
    },(err)=>{
      this.onUpdatGameModalClose();
      sendMessageUserModal(`doslo je do greske kod menjanja igre ${item.id}`);
      this.initEmptyInputUpdateForm();
    });
    
  }

  /**
   * Used to reset all fields and to close the Input Dialog.
   */
  onUpdatGameModalClose():void{
    updateGameModalShow(false);
    this.inputGameData = [];
    this.formInputInvalid = false;
    this.initEmptyInputUpdateForm();
  }

  /**
   * Deletes the row in the database that have data about one choosen game.
   * @param id {number} contains ID of the Game to delete from the database.
   */
  onDelete(id:number):void{
      console.log(id)
      this.gameService.removeGame(id).subscribe(  (e) => { sendMessageUserModal(`igra je obrisana ${id}`); this.initMainDataTable(); }
                                                  ,
                                                  (err) => { sendMessageUserModal(`doslo je do greske prilikom brisanja igre ${id}`); }
                                               );
  }

  /**
   * Sets the Input Dialog with chosen game values and starts the dialog.
   * @param {Game} item  contains the data about the GAME data that we want to update.
   */
  onUpdateOpenDialog(item:Game):void{
    this.isCreateModalActive = false;
    updateGameModalShow(true);
    this.inputGameData = JSON.parse(item.data);
    this.inputNameGameData = item.name;
    this.inputIDClicked = item.id;
  }
  /**
   * Getter
   * @returns {Array<any>} gameTableList attribute 
   */
  getGameTableList(){
    return this.gameTableList;
  }
  /**
   * Checks if all fields are filled by the end user in our Input Dialog.
   * @returns {boolean} value
   */
  checkIfFieldsNotEmpty():boolean{
    let returnBool = true;
    if(this.inputNameGameData.trim() === ""){
      return false;
    }
    this.inputGameData.forEach((e)=>{
      if (e.columnOne.trim() === "" || e.columnTwo.trim() === ""){
        returnBool = false
      }
    });
    return returnBool;
  }

  /**
   * Empties out all form fields and opens out Input Dialog.
   */
  onCreateNewGame(){
    this.initEmptyInputUpdateForm();
    this.isCreateModalActive = true;
    updateGameModalShow(true);
    this.inputNameGameData = "";
  }

  /**
   * Handles the submit User data.
   * Checks if fields are not empty, if they are returns nothing and stops the method execution
   * if fields are not empty it will create the Game data structure and deligate that objecct to the GameService pushGame function
   */
  onCreateNewGameSubmit(){
    if(!this.checkIfFieldsNotEmpty()){
      this.formInputInvalid = true;
      return;
    } else{ this.formInputInvalid = false;}
    this.formInputInvalid = false;

    let item = {
      name: this.inputNameGameData,
      data: JSON.stringify(this.inputGameData)
    }
    this.gameService.pushGame(item).subscribe((data)=>{
      this.onUpdatGameModalClose();
      sendMessageUserModal("Nova Igra Uspesno Napravljena");
      this.initMainDataTable();
    }, (err)=>{
      sendMessageUserModal("Doslo je do greske, igra nije mogla da se napravi");
    });

  }
  /**
   * Getter
   * @returns {boolean} if out input dialog is active or not.
   */
  getIsCreateModalActive():boolean{
    return this.isCreateModalActive;
  }

}
