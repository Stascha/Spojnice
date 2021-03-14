import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GameService } from 'src/app/services/game.service';
import { UserService } from 'src/app/services/user.service';
import { GameComponent } from '../game.component';

/** JS function defined in the index.html file. Function that will initialize modal reference when called. */
declare const initializeSettingsModalVar: any;
/** JS function defined in the index.html file. Function that will show the modal to the end user. */
declare const settingsModalShow: any;

/** Component - UserActionsBtnsComponent*/
@Component({
  selector: 'app-user-actions-btns',
  templateUrl: './user-actions-btns.component.html',
  styleUrls: ['./user-actions-btns.component.css']
})
/**
 * Contains action buttons - mainly the navigation for the user.
 */
export class UserActionsBtnsComponent implements OnInit {
  /**
   * Constructor
   * @param userService {UserService}
   * @param router {Router}
   * @param gameService {GameService}
   * @param gameComponent {GameComponent}
   */
  constructor(
    public userService : UserService,
    private router:Router,
    private gameService: GameService,
    private gameComponent : GameComponent
  ) {
    
  }
  /** Setting up the settings modal variable reference */
  ngOnInit(): void {
    initializeSettingsModalVar();
  }

  /**
   * When user choose the logout option we will use method logoutUser from the UserService service class to log the user out.
   * After that user will be reddirected to our login page.
   */
  onUserLogout():void{
    this.userService.logoutUser();
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
    this.router.navigate(["/login"]));
  }

  /**
   * New game will be started (via gameComponent) and game will be set to being active (gameService - attribute: gameActive ).
   */
  onNewGame(){
    this.gameComponent.startTheGame();
    this.gameService.gameActive = true;
  }

  /**
   * Funtion that is used to open the settings modal
   */
  onSettingsModalOpen(){
    settingsModalShow(true);
  }

  /**
   * Function that is used to close our modal
   * On modal close it will update the provided notification value with our database value
   */
  onSettingsModalClose(){
    settingsModalShow(false);
  }

  /**
   * Function that will change the current notification value within our user object on the frontend
   * and will contact the function from userService that is going to contact our microservice API
   * in order to change the notifications value within our database
   */
  onSettingsNotificationCheckBoxChange(){
    this.userService.getLoggedinUser().notificationsActive = !this.userService.getLoggedinUser().notificationsActive;
    this.userService.changeEmailNotifications().subscribe()
  }
}
