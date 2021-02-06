import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";

/** Score interface representing the data */
export interface Score { // izgled score podataka
    /** Score id represents score id in our Score table in the database */
    id: number,
    /** Score score represents score in our Score table in the database */
    score: number,
    /** Score id represents score id in our Score table in the database */
    username: string
  }

@Injectable()
export class ScoreService {
    /** Contains full score list that is in our microservice database */
    private scoreList : Array<Score> = [];
    /** URL to our microservice api. */
    apiURL: string = "https://localhost:5201/api";  // glavna URL adresa za nas API
    /**
     * constructor
     * @param httpClient {HttpClient} Used to issue requests to our api calls.
     */
    constructor(private httpClient: HttpClient) {}

    /**
     * Used to contact microservice API and obtain the score list on success.
     * @returns Observable that will return full list of scores found in the microservice database
     */
    public getScore() : Observable <any>  {
        return this.httpClient
                .get(`${this.apiURL}/score`);
    }
    /**
     * Sends score to add to the microservice backend database.
     * @param score {number}
     * @param username {string}
     * @returns Observable
     */
    public pushScore(score: number, username:string) : Observable <any>  {
        let scoreObj = {
            score : score,
            username : username,
        }
        return this.httpClient
        .post(`${this.apiURL}/score`, scoreObj)
    }

    /**
     * Set method for our private class scoreList attribute
     * @param inScoreList {Array<Score>}
     */
    public setScoreList( inScoreList : Array<Score>){
        this.scoreList = inScoreList
    }
    /**
     * Getter
     * @returns private class scoreList attribute
     */
    public getScoreList(){
        return this.scoreList;
    }
    
}