import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GameService } from 'src/app/services/game.service';
import { UserService } from 'src/app/services/user.service';
import { GameComponent } from '../game.component';

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
  /** Empty ngOnInit */
  ngOnInit(): void {
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
}
