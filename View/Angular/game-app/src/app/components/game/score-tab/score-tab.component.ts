import { Component, OnInit } from '@angular/core';
import { ScoreService, Score} from 'src/app/services/score.service';

/** Component - ScoreTabComponent */
@Component({
  selector: 'app-score-tab',
  templateUrl: './score-tab.component.html',
  styleUrls: ['./score-tab.component.css']
})
/** Used to show the Score Table at the right top corner to the end user.*/
export class ScoreTabComponent implements OnInit {
  /** Contains an array of our Scores */
  scoreList : Array<Score> = [];
  /** Keeps the type of order */
  scoreSortOrder : string = "asc";
  /**
   * Constructor
   * @param scoreService {ScoreService}
   */
  constructor(public scoreService : ScoreService) { }
  /** Calls the updateScoreTableView method */
  ngOnInit(): void {
    this.updateScoreTableView();
  }

  /** Get the Score list data from scoreService */
  updateScoreTableView(){
    this.scoreService.getScore().subscribe((data)=>{
      this.scoreService.setScoreList(data);
      console.log(data)
    })
  }
 /** Used to sort our Score Table by the user choice ascending or descending order. */
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
