﻿import { HttpClient } from 'aurelia-fetch-client';
import { autoinject } from 'aurelia-framework';

@autoinject()
export class Game {

    CardsPlayer1 = ["S2", "S3", "S4", "S5", "S6", "S7", "S8", "S9", "S99", "SB", "SJ", "SK", "SZ"];
    CardsPlayer2 = ["R2", "R3", "R4", "R5", "R6", "R7", "R8", "R9", "R99", "RB", "RJ", "RK", "RZ"];
    CardsPlayer3 = ["K2", "K3", "K4", "K5", "K6", "K7", "K8", "K9", "K99", "KB", "KJ", "KK", "KZ"];
    CardsPlayer4 = ["H2", "H3", "H4", "H5", "H6", "H7", "H8", "H9", "H99", "HB", "HJ", "HK", "HZ"];
    public TrumpKinds: string[];
    public AceKinds: string[];
    public cards: string[];
    public Player1Cards: string[];
    public Player2Cards: string[];
    public Player3Cards: string[];
    public Player4Cards: string[];

    public none: string;
    public nohide: string;
    public ShowRound2: string;
    public turn: string;
    public ShowTeams: string;
    

    public CurrentGameID: number;
    public players: string[];

    public playerIDs: number[];

    public Player1Bid: string[];
    public Player2Bid: string[];
    public Player3Bid: string[];
    public Player4Bid: string[];

    public ChoicesPlayer: string[];

    public BidsGame: string[];
    public PlayingField: string;

    public TrumpAce: string[];
    public Trump: string;
    public Ace: string;
    public TrumpAcehide: string;

    public PlayingCard1: string;
    public PlayingCard2: string;
    public PlayingCard3: string;
    public PlayingCard4: string;

    constructor(private http: HttpClient) {

    }



    public async activate() {

        let result = await this.http.fetch('api/Player/allplayers');
        this.players = await result.json() as Array<string>;
        this.nohide = "none";
        this.ShowRound2 = "none";
        this.PlayingField = "none";
        this.TrumpAcehide = "none";
        this.ShowTeams = "none";
    }
    public LastHit: cardplayer[];
    public PlayersLastHit: string[];
    public CardsLastHit: string[];
    public async ShowLastHit() {
        let result = await this.http.fetch('api/Hit/ShowLastHit/' + this.CurrentGameID);
        this.LastHit = await result.json() as cardplayer[];

        let Result = await this.http.fetch('api/Player/GetPlayerNames/' + this.LastHit[0].playerID + '/' + this.LastHit[1].playerID + '/' + this.LastHit[2].playerID + '/' + this.LastHit[3].playerID);
        this.PlayersLastHit = await Result.json() as Array<string>;

        let REsult = await this.http.fetch('api/Game/GetCardNames/' + this.LastHit[0].cardID + '/' + this.LastHit[1].cardID + '/' + this.LastHit[2].cardID + '/' + this.LastHit[3].cardID);
        this.CardsLastHit = await REsult.json() as Array<string>;

  
        alert(this.PlayersLastHit[0] + " had " + this.CardsLastHit[0] + "\n" + this.PlayersLastHit[1] + " had " + this.CardsLastHit[1] + "\n" + this.PlayersLastHit[2] + " had " + this.CardsLastHit[2] + "\n" + this.PlayersLastHit[3] + " had " + this.CardsLastHit[3]);

 }
    public async Player1Plays(card: string) {
        await this.http.fetch('api/Hit/PlayedCard/' + this.CurrentGameID + '/' + this.playerIDs[0] + '/' + card);
        this.PlayingCard1 = card;
        var index = this.CardsPlayer1.indexOf(card);
        this.CardsPlayer1.splice(index, 1);

        if (card == this.askedAce) {
            await this.http.fetch('api/Game/UpdateTeams/' + this.playerIDs[0] + '/' + this.CurrentGameID);
            await this.GetTeam1();
            await this.GetTeam2();
        }
        
        if (this.PlayingCard1 != "" && this.PlayingCard2 != "" && this.PlayingCard3 != "" && this.PlayingCard4 != "") {
            await this.http.fetch('api/Hit/WhoWonBid/' + this.CurrentGameID);
            await this.http.fetch('api/Hit/Delay');
            this.PlayingCard1 = "";
            this.PlayingCard2 = "";
            this.PlayingCard3 = "";
            this.PlayingCard4 = "";
            this.GetAllHits();
        }

    }
    public async Player2Plays(card: string) {
        await this.http.fetch('api/Hit/PlayedCard/' + this.CurrentGameID + '/' + this.playerIDs[1] + '/' + card);
        this.PlayingCard2 = card;
        var index = this.CardsPlayer2.indexOf(card);
        this.CardsPlayer2.splice(index, 1);
        if (card == this.askedAce) {
            await this.http.fetch('api/Game/UpdateTeams/' + this.playerIDs[1] + '/' + this.CurrentGameID);
            await this.GetTeam1();
            await this.GetTeam2();
        }
        if (this.PlayingCard1 != "" && this.PlayingCard2 != "" && this.PlayingCard3 != "" && this.PlayingCard4 != "") {
            await this.http.fetch('api/Hit/WhoWonBid/' + this.CurrentGameID);
            await this.http.fetch('api/Hit/Delay');
            this.PlayingCard1 = "";
            this.PlayingCard2 = "";
            this.PlayingCard3 = "";
            this.PlayingCard4 = "";
            this.GetAllHits();
            // await this.http.fetch('api/Hit/WhoWonBid/' + this.PlayingCard1 + '/' + this.PlayingCard2 + '/' + this.PlayingCard3 + '/' + this.PlayingCard4);
        }

    }
    public async Player3Plays(card: string) {
        await this.http.fetch('api/Hit/PlayedCard/' + this.CurrentGameID + '/' + this.playerIDs[2] + '/' + card);
        this.PlayingCard3 = card;
        var index = this.CardsPlayer3.indexOf(card);
        this.CardsPlayer3.splice(index, 1);
        if (card == this.askedAce) {
            await this.http.fetch('api/Game/UpdateTeams/' + this.playerIDs[2] + '/' + this.CurrentGameID);
            await this.GetTeam1();
            await this.GetTeam2();
            this.GetAllHits();
        }
        if (this.PlayingCard1 != "" && this.PlayingCard2 != "" && this.PlayingCard3 != "" && this.PlayingCard4 != "") {
            await this.http.fetch('api/Hit/WhoWonBid/' + this.CurrentGameID);
            await this.http.fetch('api/Hit/Delay');
            this.PlayingCard1 = "";
            this.PlayingCard2 = "";
            this.PlayingCard3 = "";
            this.PlayingCard4 = "";
            this.GetAllHits();
            //await this.http.fetch('api/Hit/WhoWonBid/' + this.PlayingCard1 + '/' + this.PlayingCard2 + '/' + this.PlayingCard3 + '/' + this.PlayingCard4);
        }

    }
    public async Player4Plays(card: string) {
        await this.http.fetch('api/Hit/PlayedCard/' + this.CurrentGameID + '/' + this.playerIDs[3] + '/' + card);
        this.PlayingCard4 = card;
        var index = this.CardsPlayer4.indexOf(card);
        this.CardsPlayer4.splice(index, 1);
        if (card == this.askedAce) {
            await this.http.fetch('api/Game/UpdateTeams/' + this.playerIDs[3] + '/' + this.CurrentGameID);
            await this.GetTeam1();
            await this.GetTeam2();
        }
        if (this.PlayingCard1 != "" && this.PlayingCard2 != "" && this.PlayingCard3 != "" && this.PlayingCard4 != "") {
            await this.http.fetch('api/Hit/WhoWonBid/' + this.CurrentGameID);
            await this.http.fetch('api/Hit/Delay');
            this.PlayingCard1 = "";
            this.PlayingCard2 = "";
            this.PlayingCard3 = "";
            this.PlayingCard4 = "";
            this.GetAllHits();
            // await this.http.fetch('api/Hit/WhoWonBid/' + this.PlayingCard1 + '/' + this.PlayingCard2 + '/' + this.PlayingCard3 + '/' + this.PlayingCard4);
        }

    }
    public slagenSpelers: number[];
    public slagenSpeler1: number;
    public slagenSpeler2: number;
    public slagenSpeler3: number;
    public slagenSpeler4: number;

    public async GetAllHits() {
        let result = await this.http.fetch('api/Hit/GetAllHits/' + this.CurrentGameID);
        this.slagenSpelers = await result.json() as Array<number>;
        this.slagenSpeler1 = this.slagenSpelers[0];
        this.slagenSpeler2 = this.slagenSpelers[1];
        this.slagenSpeler3 = this.slagenSpelers[2];
        this.slagenSpeler4 = this.slagenSpelers[3];
    }

    public GetIDPlayer() {
        if (this.turn == "Player1") {
            return this.playerIDs[0];
        }
        else if (this.turn == "Player2") {
            return this.playerIDs[1];
        }
        else if (this.turn == "Player3") {
            return this.playerIDs[2];
        }
        else if (this.turn == "Player4") {
            return this.playerIDs[3];
        }
    }
    public async setTeams() {
        let playerid = this.GetIDPlayer();
        await this.http.fetch('api/Game/SetTeam/' + this.CurrentGameID + '/' + playerid + '/' + this.GameTypeGame);
        
    }

    public askedAce: string;
    public async GetTrumpAndAce() {
        let result = await this.http.fetch('api/Game/GetTrumpAce/' + this.CurrentGameID);
        this.TrumpAce = await result.json() as Array<string>;
        this.Trump = this.TrumpAce[0];
        this.Ace = this.TrumpAce[1];
        this.TrumpAcehide = "";

        if (this.Ace == "Clubs") {
            this.askedAce = "KZ";
        }
        else if (this.Ace == "Hearts") {
            this.askedAce = "HZ";
        }
        else if (this.Ace == "Diamonds") {
            this.askedAce = "RZ";
        }
        else if (this.Ace == "Spades") {
            this.askedAce = "SZ";
        }

    }
    public async StartGame(Trump: string, Ace: string) {

        if (Trump == Ace) {
            alert("you cant ask for this ace if your trump is the same!");
        }
        else {
            var re = /alleen/gi;
            var Re = /misère/gi;
            if (this.GameTypeGame.search(re) != -1) {
                Ace = "No ace";
            }
            if (this.GameTypeGame.search(Re) != -1) {
                Ace = "No ace";
                Trump = "No trump";
            }
            let result = await this.http.fetch('api/Game/UpdateGame/' + Trump + '/' + Ace + '/' + this.GameTypeGame + '/' + this.CurrentGameID);
            alert("the cards can be played now! the player in red is on the move");
            await this.setTeams();
            //method to decide who's turn it is
            //for now it is always player1
            this.turn = "Player1";
            this.PlayingField = "";
            this.ShowRound2 = "none";
            this.ShowTeams = "";
            await this.GetTrumpAndAce();
            await this.GetTeam1();
            await this.GetTeam2();
        }
       
    }
    public Team1: string[];
    public Team2: string[];

    public async GetTeam1() {
        let result = await this.http.fetch('api/Game/GetTeam1/' + this.CurrentGameID);
        this.Team1 = await result.json() as Array<string>;
    }
    public async GetTeam2() {
        let result = await this.http.fetch('api/Game/GetTeam2/' + this.CurrentGameID);
        this.Team2 = await result.json() as Array<string>;
    }

    public async BeginGame(Player1: string, Player2: string, Player3: string, Player4: string) {

        await this.http.fetch('api/Game/BeginGame/' + Player1 + '/' + Player2 + '/' + Player3 + '/' + Player4);

        this.none = "none";
        this.nohide = "";

        this.PlayingCard1 = "";
        this.PlayingCard2 = "";
        this.PlayingCard3 = "";
        this.PlayingCard4 = "";

        //reset all data 
        this.cards = [];
        this.Player1Cards = [];
        this.Player2Cards = [];
        this.Player3Cards = [];
        this.Player4Cards = [];

        this.TrumpAce = [];
        this.Trump = "";
        this.Ace = ""; 
        this.CurrentGameID = 0;


        this.Player1Bid = [];
        this.Player2Bid = [];
        this.Player3Bid = [];
        this.Player4Bid = [];

        this.ChoicesPlayer = ["pas", "rik", "misère"];
        this.askedAce = "";



        //decide who's turn it is, for now it's always player1.
        //player 1 can always make his choice
        this.turn = "Player1";

        //get all data
        await this.GetData();

    }

    public async GetChoicesPlayer() {

        let result = await this.http.fetch('api/Game/GetChoicesPlayer/' + this.CurrentGameID);

        //bepalen van de hoogste bieding
        this.ChoicesPlayer = await result.json() as Array<string>;
    }

    public async SubmitChoice(choice: string) {

        var bool = true;
        if (this.turn == "Player1") {

            await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID + '/' + choice + '/' + this.playerIDs[0]);
            await this.getBidPlayer(this.turn);
            this.turn = "Player2";
            this.turn = await this.CanPlayerSubmit();
            await this.GetChoicesPlayer();

        }
        else if (this.turn == "Player2") {

            await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID + '/' + choice + '/' + this.playerIDs[1]);
            await this.getBidPlayer(this.turn);
            this.turn = "Player3";
            this.turn = await this.CanPlayerSubmit();
            await this.GetChoicesPlayer();

        }
        else if (this.turn == "Player3") {

            await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID + '/' + choice + '/' + this.playerIDs[2]);
            await this.getBidPlayer(this.turn);
            this.turn = "Player4";
            this.turn = await this.CanPlayerSubmit();
            await this.GetChoicesPlayer();
        }
        else if (this.turn == "Player4") {

            await this.http.fetch('api/Game/SubmitChoice/' + this.CurrentGameID + '/' + choice + '/' + this.playerIDs[3]);
            await this.getBidPlayer(this.turn);
            this.turn = "Player1";
            this.turn = await this.CanPlayerSubmit();
            await this.GetChoicesPlayer();

        }


    }




    public GameTypeGame: string;
    public GameTypePlayer: number;



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
                this.nohide = "none";
                this.ShowRound2 = "";
                

                for (var i = 0; i < 4; i++) {
                    if (this.GameTypePlayer == this.playerIDs[i]) {
                        if (i == 0) {
                            this.turn = "Player1";
                        }
                        else if (i == 1) {
                            this.turn = "Player2";
                        }
                        else if (i == 2) {
                            this.turn = "Player3";
                        }
                        else {
                            this.turn = "Player4";
                        }
                    }
                }

              
                var Re = /alleen/gi;
                var RE = /misère/gi;
                var re = /beter/gi;
                if (this.GameTypeGame.search(re) != -1) {
                    this.TrumpKinds = ["Hearts"];
                    this.AceKinds = ["Diamonds", "Hearts", "Spades", "Clubs"];
                }
                else if (this.GameTypeGame.search(Re) != -1) {
                    this.AceKinds = [];
                    this.TrumpKinds = ["Diamonds", "Hearts", "Spades", "Clubs"];
                }
                else if (this.GameTypeGame.search(RE) != -1) {
                    this.AceKinds = [];
                    this.TrumpKinds = [];
                }
                else {
                    this.AceKinds = ["Diamonds", "Hearts", "Spades", "Clubs"];
                    this.TrumpKinds = ["Diamonds", "Hearts", "Spades", "Clubs"];
                }
            }

        }
        else if (passes == 4) {
            this.BeginGame(this.players[0], this.players[1], this.players[2], this.players[3]);
            alert("There is shared again");
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
        return this.turn;
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

        await this.GetPlayerNames();
    }
    public PlayerNames: string[];

    public async GetPlayerNames() {
        let result = await this.http.fetch('api/Player/GetPlayerNames/' + this.playerIDs[0] + '/' + this.playerIDs[1] + '/' + this.playerIDs[2] + '/' + this.playerIDs[3]);
        this.PlayerNames = await result.json() as Array<string>;
    }

    //bepalen van de hoogste bieding
    public async GetHighestBidInGame() {
        let result = await this.http.fetch('api/Game/GetHighestBidInGame/' + this.CurrentGameID);
        this.HighestBid = await result.json() as Bieding;
        this.GameTypePlayer = this.HighestBid.playerID;
        this.GameTypeGame = this.HighestBid.gameTypeName;
    }
    public HighestBid: Bieding;
    public Bids: Bieding[];
 

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

interface cardplayer {
    cardPlayerID: number;
    hitID: number;
    playerID: number;
    gameID: number;
    cardID: number;
}

