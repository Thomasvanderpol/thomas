import { HttpClient } from 'aurelia-fetch-client';
import { autoinject } from 'aurelia-framework';

@autoinject()
export class Game {

    CardsPlayer1 = ["S2", "S3", "S4", "S5", "S6", "S7", "S8", "S9", "S99", "SB", "SJ", "SK", "SZ"];
    CardsPlayer2 = ["R2", "R3", "R4", "R5", "R6", "R7", "R8", "R9", "R99", "RB", "RJ", "RK", "RZ"];
    CardsPlayer3 = ["K2", "K3", "K4", "K5", "K6", "K7", "K8", "K9", "K99", "KB", "KJ", "KK", "KZ"];
    CardsPlayer4 = ["H2", "H3", "H4", "H5", "H6", "H7", "H8", "H9", "H99", "HB", "HJ", "HK", "HZ"];
    public cards: string[];
    public Player1Cards: string[];
    public Player2Cards: string[];
    public Player3Cards: string[];
    public Player4Cards: string[];
    public none: string;
    public nohide: string;
    public turn: string;
    public ChoicePlayer1: string;
    public ChoicePlayer2: string;
    public ChoicePlayer3: string;
    public ChoicePlayer4: string;
    public CurrentGameID: number;
    public players: string[];
    public CurrentPlayerIDs: number[];

    constructor(private http: HttpClient) {

    }



    public async activate() {

        let result = await this.http.fetch('api/Player/allplayers');
        this.players = await result.json() as Array<string>;
        this.nohide = "none";

    }


    public async BeginGame(Player1: string, Player2: string, Player3: string, Player4: string) {

        await this.http.fetch('api/Game/BeginGame/' + Player1 + '/' + Player2 + '/' + Player3 + '/' + Player4);
       
        this.none = "none";
        this.nohide = "";
        this.turn = "Player1";
        await this.GetData();

    }
    public async GetCards() {
        let result = await this.http.fetch('api/Game/GetCards');
        this.cards = await result.json() as Array<string>;

        this.CardsPlayer1 = this.cards.slice(0, 13);
        this.CardsPlayer1.sort();

        this.CardsPlayer2 = this.cards.slice(13, 26);
        this.CardsPlayer2.sort();

        this.CardsPlayer3 = this.cards.slice(26, 39);
        this.CardsPlayer3.sort();

        this.CardsPlayer4 = this.cards.slice(39, 52);
        this.CardsPlayer4.sort();


    }

    public async SubmitChoice(choice: string) {

        if (this.turn == "Player1") {
            this.ChoicePlayer1 = choice;
            this.turn = "Player2";
            let result = await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID) + '/' + this.ChoicePlayer1 + '/' + this.CurrentPlayerIDs[0];
        }
        else if (this.turn == "Player2") {
            this.ChoicePlayer2 = choice;
            this.turn = "Player3";
            let result = await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID) + '/' + this.ChoicePlayer2 + '/' + this.CurrentPlayerIDs[1];
        }
        else if (this.turn == "Player3") {
            this.ChoicePlayer3 = choice;
            this.turn = "Player4";
            let result = await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID) + '/' + this.ChoicePlayer3 + '/' + this.CurrentPlayerIDs[2];
        }
        else if (this.turn == "Player4") {
            this.ChoicePlayer4 = choice;
            this.turn = "Player1";
            let result = await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID) + '/' + this.ChoicePlayer4 + '/' + this.CurrentPlayerIDs[3];
        }
    }

    public async GetData() {
        await this.GetCards();

        let Result = await this.http.fetch('api/Game/GetPlayersIDs');
        this.CurrentPlayerIDs = await Result.json() as Array<number>;
        
        let result = await this.http.fetch('api/Game/GetCurrentGameID');
        this.CurrentGameID = await result.json() as number;



    }





}
