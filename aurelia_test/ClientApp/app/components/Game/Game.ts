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
    public amountpasses: number = 0;

    public CurrentGameID: number;
    public players: string[];

    public playerIDs: number[];

    public Player1Bid: string[];
    public Player2Bid: string[];
    public Player3Bid: string[];
    public Player4Bid: string[];

    public BidsGame: string[];
    public HighestBidGame: string;




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

        //reset all data 
        this.cards = [];
        this.Player1Cards = [];
        this.Player2Cards = [];
        this.Player3Cards = [];
        this.Player4Cards = [];

        this.nohide = "";


        this.CurrentGameID = 0;


        this.Player1Bid = [];
        this.Player2Bid = [];
        this.Player3Bid = [];
        this.Player4Bid = [];
        this.BidsGame = [];
        this.HighestBidGame = "";



        //bepaal wiens beurt het is, voor nu zet ik hem altijd op player 1
        this.turn = "Player1";

        //get all data
        await this.GetData();


        //can the player who is on turn. can he submit? if not:  player turn is next. if true: give him the choices
        await this.CanPlayerSubmit();

    }


    public async SubmitChoice(choice: string) {


        if (this.turn == "Player1") {
            await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID + '/' + choice + '/' + this.playerIDs[0]);
            await this.getBidPlayer();
            this.turn = "Player2";
            await this.CanPlayerSubmit();
        }
        else if (this.turn == "Player2") {
            await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID + '/' + choice + '/' + this.playerIDs[1]);
            await this.getBidPlayer();
            this.turn = "Player3";
            await this.CanPlayerSubmit();
        }
        else if (this.turn == "Player3") {
            await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID + '/' + choice + '/' + this.playerIDs[2]);
            await this.getBidPlayer();
            this.turn = "Player4";
            await this.CanPlayerSubmit();
        }
        else if (this.turn == "Player4") {
            await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID + '/' + choice + '/' + this.playerIDs[3]);
            await this.getBidPlayer();
            this.turn = "Player1";
            await this.CanPlayerSubmit();
        }


    }



    public async getBidPlayer() {
        let player = 0;
        if (this.turn == "Player1") {
            player = this.playerIDs[0];
            let result = await this.http.fetch('api/Game/GetBidPlayer/' + player + '/' + this.CurrentGameID);
            this.Player1Bid = await result.json() as Array<string>;
        }
        else if (this.turn == "Player2") {
            player = this.playerIDs[1];
            let result = await this.http.fetch('api/Game/GetBidPlayer/' + player + '/' + this.CurrentGameID);
            this.Player2Bid = await result.json() as Array<string>;
        }
        else if (this.turn == "Player3") {
            player = this.playerIDs[2];
            let result = await this.http.fetch('api/Game/GetBidPlayer/' + player + '/' + this.CurrentGameID);
            this.Player3Bid = await result.json() as Array<string>;
        }
        else if (this.turn == "Player4") {
            player = this.playerIDs[3];
            let result = await this.http.fetch('api/Game/GetBidPlayer/' + player + '/' + this.CurrentGameID);
            this.Player4Bid = await result.json() as Array<string>;
        }
    }




    public async CanPlayerSubmit() {
       
    }



    // alle data voor het starten van een game
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

    public async GetCurrentGameID() {
        let result = await this.http.fetch('api/Game/GetCurrentGameID');
        this.CurrentGameID = await result.json() as number;
    }

    public async GetPlayerIDs() {
        let result = await this.http.fetch('api/Game/GetPlayersIDs');
        this.playerIDs = await result.json() as Array<number>;
    }


    public async GetData() {
        await this.GetCards();

        await this.GetPlayerIDs();

        await this.GetCurrentGameID();
    }

    //bepalen van de hoogste bieding
    public async GetHighestBidInGame() {
        let result = await this.http.fetch('api/Game/GetHighestBidInGame/' + this.CurrentGameID);
        this.HighestBidGame = await result.json() as string;
    }

    //alle biedingen in de game
    public async GetBidsCurrentGame() {
        let result = await this.http.fetch('api/Game/GetBidsCurrentGame/' + this.CurrentGameID);
        this.BidsGame = await result.json() as Array<string>;
    }

}


