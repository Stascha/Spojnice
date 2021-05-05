import { Component, OnInit } from '@angular/core';
import { GameService } from 'src/app/services/game.service';
/**  Component IndexComponent*/
@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})

/**
 *  prva komponenta koja se ucitava kada je korisnik na glavnoj strani tj. kada se otvori glavni URL web sajta
 */
export class IndexComponent implements OnInit
{
  /**
   * Konstruktor
   * @param gameService {GameService}
   */
  constructor( private gameService: GameService) { }

  /** 
   * Postavlja gameService - gameActive atribut na false
   */
    ngOnInit(): void
    {
      this.gameService.gameActive = false;
    }

}
