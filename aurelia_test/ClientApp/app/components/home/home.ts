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
        this.getPlayers();
    }
    
    public async getPlayers() {

        let result = await this.http.fetch('api/Player/allplayers');
        this.players = await result.json() as Array<string>;

    }
    
    public async activate() {

        let result = await this.http.fetch('api/Player/allplayers');
        this.players = await result.json() as Array<string>;
    }
    
    public async BeginGame(Player1: string, Player2: string, Player3: string, Player4: string) {
        
        let result = await this.http.fetch('api/Game/BeginGame/' + Player1 + '/' + Player2 + '/' + Player3 + '/' + Player4);

        this.gelukt = result.toString();

        if (this.gelukt == "1") {
            this.beginGame = "The 4 players are registrated, begin your game!";
        }
        else {
            this.beginGame = "Something went wrong!";
        }
        

    }
}
