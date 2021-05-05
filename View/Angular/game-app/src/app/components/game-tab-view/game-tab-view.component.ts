import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Game, GameService } from 'src/app/services/game.service';
import { UserService } from 'src/app/services/user.service';
/** JS funkcija definisana u index.html fajlu. Funkcija koja inicijalizuje modal kada se pozove. */
declare const initializeUpdateGameModalVar: any;
/** JS funkcija definisana u index.html fajlu. Funkcija koja prikazuje modal. */
declare const updateGameModalShow: any;
/** JS funkcija definisana u index.html fajlu. Funkcija koja prikazuje modal sa tekstom. */
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
 * GameTabViewComponent - stranica koja omogucava adminu da menja, brise i kreira nove igre
 */
export class GameTabViewComponent implements OnInit {
  /** Sadrzi podatke o igri koji nisu prikazani na pocetku.*/
  private gameTableList : Array<any> = [];
  /** Sadrzi niz polja za unos podataka za menjanje igre ili za kreiranje nove igre. */
  public inputGameData : Array<any> = [];
  /** Sadrzi naziv igre kada se menja igra ili kada se kreira nova igra */
  public inputNameGameData : string = "";
  /**Sadrzi informaciju da li su sva polja popunjena na panelu za unos podataka, kada se menja ili kreira nova igra. */
  public formInputInvalid : boolean = false;
  /** Kada se menja igra, to polje sadrzi ID igre u bazi podataka. */
  private inputIDClicked: number = -1;
  /** Informacija da li je panel za unos podataka aktivan bez obzira da li se menja igra ili se kreira nova igra. */
  private isCreateModalActive : boolean = false;
  /**
   * Konstruktor : ako igrac nije admin, igrac ce biti redirektovan na login stranicu.
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

    /** Isprazni sva polja na panelu za unos podataka */
   initEmptyInputUpdateForm(){
     // postavlja sva polja za izmene na prazne stringove
    this.inputGameData = [];
    for(let i = 0; i < 10;i++){
      this.inputGameData.push({columnOne: "", columnTwo: ""});
    }
   }

   /**  Method koristi gameService da uzme podatke o svim igrama i prikazuje ih igracima */
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
   * Postavlja vrednost gameEndedFlag attributa iz GameService klase na true.
   * poziva metod initMainDataTable.
   */
  ngOnInit(): void {
    this.gameService.setGameEndedFlag(true);
    this.initMainDataTable();
    
    initializeUpdateGameModalVar();
  }
  /**
   * Metod se poziva kada igrac odabere da promeni igru.
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
   * Resetuje sva polja i zatvara Input panel za promenu igre.
   */
  onUpdatGameModalClose():void{
    updateGameModalShow(false);
    this.inputGameData = [];
    this.formInputInvalid = false;
    this.initEmptyInputUpdateForm();
  }

  /**
   * Brise vrstu iz baze podataka koja sadrzi podatke o izabranoj igri. 
   * @param id {number} Sadrzi id igre koja se brise iz baze podataka.
   */
  onDelete(id:number):void{
      console.log(id)
      this.gameService.removeGame(id).subscribe(  (e) => { sendMessageUserModal(`igra je obrisana ${id}`); this.initMainDataTable(); }
                                                  ,
                                                  (err) => { sendMessageUserModal(`doslo je do greske prilikom brisanja igre ${id}`); }
                                               );
  }

  /**
   * Prikazuje panel sa podacima izabrane igre.
   * @param {Game} item  Sadrzi podatke o igri koja se menja.
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
   * @returns {Array<any>} gameTableList atribut 
   */
  getGameTableList(){
    return this.gameTableList;
  }
  /**
   * Proverava da li su sva polja na panelu za unos podataka popunjena.
   * @returns {boolean} vrednost
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
   * Isprazni sva polja panela za unos podataka i prikaze panel za unos podataka.
   */
  onCreateNewGame(){
    this.initEmptyInputUpdateForm();
    this.isCreateModalActive = true;
    updateGameModalShow(true);
    this.inputNameGameData = "";
  }

  /**
   * Proverava da li su sva polja popunjena.
   * Ako nisu sva polja popunjena zaustavlja izvrsavanje metode. 
   * Ako su sva polja popunjena kreirace strukturu podataka igre i prosledice taj objekat GameService pushGame funkciji
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
   * @returns {boolean} podatak da li je panel za unos podataka aktivan ili ne.
   */
  getIsCreateModalActive():boolean{
    return this.isCreateModalActive;
  }

}
