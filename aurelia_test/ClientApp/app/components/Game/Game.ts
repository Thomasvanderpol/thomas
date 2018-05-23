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

    public ChoicesPlayer: string[];

    public BidsGame: string[];
  




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

        this.ChoicesPlayer = ["pas", "rik", "misère"];
       



        //decide who's turn it is, for now it's always player1.
        //player 1 can always make his choice
        this.turn = "Player1";

        //get all data
        await this.GetData();

    }


    public async SubmitChoice(choice: string) {

        var bool = true;
        if (this.turn == "Player1") {

            await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID + '/' + choice + '/' + this.playerIDs[0]);
            await this.getBidPlayer(this.turn);
            this.turn = "Player2";
            this.turn = await this.CanPlayerSubmit();


        }
        else if (this.turn == "Player2") {

            await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID + '/' + choice + '/' + this.playerIDs[1]);
            await this.getBidPlayer(this.turn);
            this.turn = "Player3";
            this.turn = await this.CanPlayerSubmit();



        }
        else if (this.turn == "Player3") {

            await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID + '/' + choice + '/' + this.playerIDs[2]);
            await this.getBidPlayer(this.turn);
            this.turn = "Player4";
            this.turn = await this.CanPlayerSubmit();


        }
        else if (this.turn == "Player4") {

            await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID + '/' + choice + '/' + this.playerIDs[3]);
            await this.getBidPlayer(this.turn);
            this.turn = "Player1";
            this.turn = await this.CanPlayerSubmit();


        }


    }




    public GameTypeGame: string;
    public GameTypePlayer: number;

    public CanPlayer1Submit: boolean = true;
    public CanPlayer2Submit: boolean = true;
    public CanPlayer3Submit: boolean = true;
    public CanPlayer4Submit: boolean = true;


    public async CanPlayerSubmit() {

        //return string wie wel aan de beurt mag komen!!!
        //haal een lijst met biedingen op van huidige game

        await this.GetBidsCurrentGame();
        var passes = 0;





        for (var i = 0; i < this.Bids.length; i++) {

            if (this.Bids[i].gameTypeName == "pas") {
                passes = passes + 1;
            }
        }
        if (passes == 3) {

            //methode aanroepen die de hoogste bieding uithaalt, bij pas is volgende speler aan de beurt.
            
            await this.GetHighestBidInGame();
            if (this.GameTypeGame == "pas") {
                //kijk welke speler nog niks heeft ingevoerd
                //die speler moet nog kiezen this.turn
                for (var i = 0; i < this.Bids.length; i++) {
                    if (this.playerIDs[i] != this.Bids[0].playerID && this.playerIDs[i] != this.Bids[1].playerID && this.playerIDs[i] != this.Bids[2].playerID) {
                        if (i == 0) {
                            this.turn = "Player1";
                        }
                        else if (i == 1) {
                            this.turn = "Player2";
                        }
                        else if (i == 2) {
                            this.turn = "Player3";
                        }
                        else if (i == 3) {
                            this.turn = "Player4";
                        }
                    }
                }
            }
            else {
                this.GameTypePlayer = this.HighestBid.playerID;
                this.GameTypeGame = this.HighestBid.gameTypeName;
                //method to start voorbereidingsronde 2!
            }

        }
        else if (passes == 4) {
            this.BeginGame(this.players[0], this.players[1], this.players[2], this.players[3]);
        }

        else {
            if (this.turn == "Player1") {
                for (var i = 0; i < this.Bids.length; i++) {

                    if (this.Bids[i].playerID == this.playerIDs[0]) {
                        if (this.Bids[i].gameTypeName == "pas") {
                            this.turn = "Player2";
                            await this.CanPlayerSubmit();
                            break;
                        }

                    }
                }
                return this.turn;

            }

            else if (this.turn == "Player2") {
                for (var i = 0; i < this.Bids.length; i++) {

                    if (this.Bids[i].playerID == this.playerIDs[1]) {
                        if (this.Bids[i].gameTypeName == "pas") {
                            this.turn = "Player3";
                            await this.CanPlayerSubmit();
                            break;
                        }

                    }
                }
                return this.turn;

            }

            else if (this.turn == "Player3") {
                for (var i = 0; i < this.Bids.length; i++) {

                    if (this.Bids[i].playerID == this.playerIDs[2]) {
                        if (this.Bids[i].gameTypeName == "pas") {
                            this.turn = "Player4";
                            await this.CanPlayerSubmit();
                            break;
                        }

                    }
                }
                return this.turn;

            }

            else if (this.turn == "Player4") {
                for (var i = 0; i < this.Bids.length; i++) {

                    if (this.Bids[i].playerID == this.playerIDs[3]) {
                        if (this.Bids[i].gameTypeName == "pas") {
                            this.turn = "Player1";
                            await this.CanPlayerSubmit();
                            break;
                        }

                    }
                }
                return this.turn;

            }


        }
        return this.turn
    }


    public async getBidPlayer(player: string) {

        let Player = 0;
        if (player == "Player1") {
            Player = this.playerIDs[0];
        }
        else if (player == "Player2") {
            Player = this.playerIDs[1];
        }
        else if (player == "Player3") {
            Player = this.playerIDs[2];
        }
        else if (player == "Player4") {
            Player = this.playerIDs[3];
        }

        let result = await this.http.fetch('api/Game/GetBidPlayer/' + Player + '/' + this.CurrentGameID);

        if (player == "Player1") {
            this.Player1Bid = await result.json() as Array<string>;
        }
        else if (player == "Player2") {
            this.Player2Bid = await result.json() as Array<string>;
        }
        else if (player == "Player3") {
            this.Player3Bid = await result.json() as Array<string>;
        }
        else if (player == "Player4") {
            this.Player4Bid = await result.json() as Array<string>;
        }



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
        this.HighestBid = await result.json() as Bieding;
        
    }
    public HighestBid: Bieding;
    public Bids: Bieding[];
    public testbid: string;
    public testID: number;
    public TestBid: string;
    public TestID: number;

    //alle biedingen in de game
    public async GetBidsCurrentGame() {
        let result = await this.http.fetch('api/Game/GetBidsCurrentGame/' + this.CurrentGameID);
        this.Bids = await result.json() as Bieding[];

    }

}

interface Bieding {
    gameTypeName: string;
    playerID: number;

}

