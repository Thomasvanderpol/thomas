import { HttpClient } from 'aurelia-fetch-client';
import { autoinject } from 'aurelia-framework';

@autoinject()
export class Home {

    public players: string[];

    public beginGame: string;
    public gelukt: string;
    constructor(private http: HttpClient) {

    }



    public async addPlayer(player1: string) {

        await this.http.fetch('api/Player/AddPlayers/' + player1);
       
    }
    

   
}
