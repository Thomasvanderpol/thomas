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
    public Bid: string;
    public CurrentGameID: number;
    public players: string[];
    public playerIDs: number[];
    public Player1Bid: string;
    public Player2Bid: string;
    public Player3Bid: string;
    public Player4Bid: string;
    public tmp: string;
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
        this.Bid = choice;

        if (this.turn == "Player1") {
            await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID + '/' + this.Bid + '/' + this.playerIDs[0]);
            await this.getBidPlayer();
            this.turn = "Player2";
            this.tmp = "test";
        }
        else if (this.turn == "Player2") {
            await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID + '/' + this.Bid + '/' + this.playerIDs[1]);
            await this.getBidPlayer();
            this.turn = "Player3";
        }
        else if (this.turn == "Player3") {
            await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID + '/' + this.Bid + '/' + this.playerIDs[2]);
            await this.getBidPlayer();
            this.turn = "Player4";
        }
        else if (this.turn == "Player4") {
            await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID + '/' + this.Bid + '/' + this.playerIDs[3]);
            await this.getBidPlayer();
            this.turn = "Player1";
        }
    
        
    }

    public async GetData() {
        await this.GetCards();

        await this.GetPlayerIDs();

        await this.GetCurrentGameID();

        
    
    }
    public async GetHighestBidInGame() {
        let result = await this.http.fetch('api/Game/GetHighestBidInGame/' + this.CurrentGameID);
        this.HighestBidGame = await result.json() as string;
    }

    public async GetBidsCurrentGame() {
        let result = await this.http.fetch('api/Game/GetBidsCurrentGame/' + this.CurrentGameID);
        this.BidsGame = await result.json() as Array<string>;
    }

    public async GetCurrentGameID() {
        let result = await this.http.fetch('api/Game/GetCurrentGameID');
        this.CurrentGameID = await result.json() as number;
    }

    public async GetPlayerIDs() {
        let result = await this.http.fetch('api/Game/GetPlayersIDs');
        this.playerIDs = await result.json() as Array<number>;
    }

    //result.json GAAT HET MIS, DIT DOET HET NIET, WEET NIET WAAROM????
    public async getBidPlayer() {
        let player = 0;
        if (this.turn == "Player1") {
            player = this.playerIDs[0];
            let result = await this.http.fetch('api/Game/GetBidPlayer/' + player + '/' + this.CurrentGameID);
            this.Player1Bid = await result.json() as string;
            this.tmp = "hallo";
        }
        else if (this.turn == "Player2") {
            player = this.playerIDs[1];
            let Result = await this.http.fetch('api/Game/GetBidPlayer/' + player + '/' + this.CurrentGameID);
            this.Player2Bid = await Result.json() as string;
        }
        else if (this.turn == "Player3") {
            player = this.playerIDs[2];
            let Result = await this.http.fetch('api/Game/GetBidPlayer/' + player + '/' + this.CurrentGameID);
            this.Player3Bid = await Result.json() as string;
        }
        else if (this.turn == "Player4") {
            player = this.playerIDs[3];
            let Result = await this.http.fetch('api/Game/GetBidPlayer/' + player + '/' + this.CurrentGameID);
            this.Player4Bid = await Result.json() as string;
        }

      
       
    }





}
