import { HttpClient } from 'aurelia-fetch-client';
import { autoinject } from 'aurelia-framework';

@autoinject()
export class Slagen {

    public Players: string[];
    public HitsByPlayer: allhits[];
    public player: string;
  
    constructor(private http: HttpClient) {

    }



    public async activate() {
        let result = await this.http.fetch('api/Player/allplayers');
        this.Players = await result.json() as Array<string>;

        //let Result = await this.http.fetch('api/Hit/GetAllHitsByPlayer');
     
    }
    public async GetAllHits(player: string) {
        this.player = player;
        let result = await this.http.fetch('api/Hit/GetAllHitsByPlayer/' + player);
        this.HitsByPlayer = await result.json() as allhits[];
        let tmp = "hallo";
    }

}
interface allhits{
    allHits: number;
    gameID: number;
}