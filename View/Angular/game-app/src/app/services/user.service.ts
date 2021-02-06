import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";
import { GameService } from "./game.service";

/** User interface representing the data */
export interface User {
    /** User id represents user id in our User table in the database */
    id         : number;
    /** User username represents user username in our User table in the database */
    username   : string;
    /** User email represents user email in our User table in the database */
    email      : string;
    /** User role represents user role in our User table in the database */
    role       : string;
    
} 

@Injectable()
export class UserService {
    // glavna URL adresa za nas API
    /** URL to our microservice api. */
    apiURL: string = "https://localhost:5101"; // glavna URL adresa za nas API
    /**This class attribute contains all the necessary user data.
     * It is used to mark if user is logged in our application later.
     * If user is not logged it contains default set of data.
     */
    private activeUser: User = { 
        // cuvamo aktivnog usera - potrebni su nam podaci
        //kako bi znali koji je user za slanje Scora,
        //kao i da li da ima dozvolu da pravi nove igre    
        id         : -1,       
        username   : "",
        email      : "",
        role       : ""
       
    };
    /**
     * constructor
     * @param httpClient {HttpClient}
     * @param gameService  {GameService} we need it to mark in case user has loggedout while playing the game to set it to be inactive.
     */
    constructor(private httpClient: HttpClient, private gameService: GameService) {} // preko httpclijenta kontaktiramo server

    /**
     * Sends the data to login our user to the application
     * @param usr {string} Username that user has entered in the input field in our Login component.
     * @param pwd {string} Password that user has entered in the input field in our Login component.
     * @returns Observable. If code is 200 it will return User object data.
     */
    public loginUser(usr:string, pwd:string) : Observable <any>  {
        let userObj = {
            username:usr,
            password:pwd
        }
        return this.httpClient
        .post(`${this.apiURL}/login`, userObj);
    }

    /**
     * Method that contacts our microservice end API to create a new user.
     * @param usr {string} Username that user has entered in the input field in our Login component.
     * @param pwd {string} Password that user has entered in the input field in our Login component.
     * @param eml {string} Email that user has entered in the input field in our Login component.
     * @param role {string} Within the application hard coded value is to be 'user' type.
     * @returns Observable
     */
    public createNewUser(usr: string, pwd: string, eml: string, role: string ): Observable<any>  {
        let userObj = {
            username: usr,
            password: pwd,           
            email   : eml,
            role    : role
        }
        return this.httpClient.post(`${this.apiURL}/create`, userObj)
    }
    /**
     * Sets default values for out activeUser atribut.
     * sets gameActive attribute to false that can be found in the Game Service.
     */
    public logoutUser(){
        this.activeUser = {           
            id         : -1,       
            username   : "",
            email      : "",
            role       : ""
           
        };
        this.gameService.gameActive = false;
    }
    /**
     * @returns activeUser attribute.
     */
    public getLoggedinUser(){
        return this.activeUser; // vraca objekat ulogovanog user-a
    }

    /**
     * Set method
     * @param dataUser {User} new value that will be added to our activeUser attribute.
     */
    public setActiveUser(dataUser: User): void{
        this.activeUser = dataUser; // postavlja aktivnog user-a
    }

}
