import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";

/** game interface model */
export interface Game { // izgled Game podataka
    /** Game id represents game id in our Game table in the database */
    id  : number,
    /** Game name represents game name in our Game table in the database */
    name: string,
    /** Game data represents game data in the JSON format in our Game table in the database */
    data: string,
  }
/**
 * Injectable GameService
 */
@Injectable()
export class GameService {
    /** Used to mark the game as ongoing or ended. */
    private gameEndedFlag : boolean = false;
    /** Used to mark if the game is currently active */
    public gameActive :boolean = true;
    /** URL to our microservice api. */
    apiURL: string = "https://localhost:5301/api"; // glavna URL adresa za nas API
    /**
     * constructor
     * @param httpClient {HttpClient} Used to issue requests to our api calls.
     */
    constructor(private httpClient: HttpClient) {} // preko httpclijenta kontaktiramo server

    /**
     * Used to contact microservice API and obtain the random list on success.
     * @returns Observable - random game object from the database, structure defiend in our Game interface
     */
    public getGame() : Observable <any>  { 
        // observable - za asinhrone funkcije Promise itd
        //https://angular.io/guide/observables
        return this.httpClient
                .get(`${this.apiURL}/game`); // vraca promise pa moramo subscribe na mestu gde pozivamo funkcije
    }
    /**
     * This method is used to create new Game in the server database
     * @param gameObj Object to send to the microservice
     */
    public pushGame(gameObj: any) : Observable <any>  {
        return this.httpClient
        .post(`${this.apiURL}/game`, gameObj) // gameObj je JS objekat koje salje kao JSON
    }
    /** 
     * Sets the game ended flag
     * @param flag {boolean} used as the new value for our gameEndedFlag attribute
     */
    public setGameEndedFlag(flag: boolean){
        this.gameEndedFlag = flag;
    }
    /**
     * Getter
     * @returns Currently active gameEndedFlag
     */
    public getGameEndedFlag(){
        return this.gameEndedFlag;
    }

    /** 
     * Updates the game with new vales
     * @param gameObj {Game} contains the new Game Object values to update 
     * @returns Observable
     */
    public updateGame(gameObj: Game): Observable <any>{
        return this.httpClient.put(`${this.apiURL}/game`, gameObj);
    }
    /** 
     * Removes the game on successful microservice api call.
     * @param id {number}
     * @returns Observable
     */
    public removeGame(id: number): Observable <any>{
        return this.httpClient.delete(`${this.apiURL}/game/${id}`);
    }
    /**
     * Used to contact microservice API and obtain the games list on success.
     * @returns Observable - List of all games that can be found in our microservice database.
     */
    public getAllGames(): Observable <any>{
        return this.httpClient
                .get(`${this.apiURL}/game/all`);
    }
}