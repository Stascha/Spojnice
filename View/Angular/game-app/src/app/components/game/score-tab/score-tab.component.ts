import { Component, OnInit } from '@angular/core';
import { ScoreService, Score} from 'src/app/services/score.service';

/** Component - ScoreTabComponent */
@Component({
  selector: 'app-score-tab',
  templateUrl: './score-tab.component.html',
  styleUrls: ['./score-tab.component.css']
})
/** Prikazuje skor tabelu u gornjem desnom uglu na panelu za igru.*/
export class ScoreTabComponent implements OnInit {
  /** Sadrzi niz skoreva */
    scoreList: Array<Score> = [];

  /** Sadrzi tip sortiranja */
    scoreSortOrder: string = "asc";

  /**
   * Konstruktor
   * @param scoreService {ScoreService}
   */
    constructor(public scoreService: ScoreService) { }

  /** Poziva updateScoreTableView metod */
  ngOnInit(): void {
    this.updateScoreTableView();
  }

  /** Uzima listu skorova iz scoreService */
  updateScoreTableView(){
    this.scoreService.getScore().subscribe((data)=>{
      this.scoreService.setScoreList(data);
      console.log(data)
    })
  }
 /** Sortira Skor tabelu u odnosu na igracev izbor u rastucem ili opadajucem poretku. */
  onScoreSort(){
    console.log(this.scoreSortOrder)
    if(this.scoreSortOrder === "asc"){
      this.scoreList = this.scoreService.getScoreList()
        .sort((a:any, b:any) => parseFloat(a.score) - parseFloat(b.score));
      this.scoreSortOrder  = "desc";
    } else if(this.scoreSortOrder === "desc"){
      this.scoreList = this.scoreService.getScoreList()
        .sort((a:any, b:any) => parseFloat(b.score) - parseFloat(a.score));
      this.scoreSortOrder  = "asc";
    }
    this.scoreService.setScoreList(this.scoreList);
  }

}
