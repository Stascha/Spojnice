import { Component, OnInit } from '@angular/core';
import { GameService } from 'src/app/services/game.service';
/**  Component IndexComponent*/
@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
/** component that loads on the root URI param of the website URL */
export class IndexComponent implements OnInit {
  /**
   * Constructor
   * @param gameService {GameService}
   */
  constructor( private gameService: GameService) { }

  /** 
   * sets the GameService - gameActive attribute to false
   */
  ngOnInit(): void {
    this.gameService.gameActive = false;
  }

}
