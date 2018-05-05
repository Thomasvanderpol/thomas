import { HttpClient } from 'aurelia-fetch-client';
import { inject } from 'aurelia-framework';

@inject(HttpClient)
export class Fetchdata {
    public forecasts: WeatherForecast[] | undefined;

    constructor(private http: HttpClient) {

    }

    public async activate() {
        let result = await this.http.fetch('api/SampleData/WeatherForecasts');
        this.forecasts = await result.json() as WeatherForecast[];
    }
}

interface WeatherForecast {
    dateFormatted: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}
