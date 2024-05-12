import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';


// Define models to be used
interface purchaseFormData {
  purchaseAmount: number;
  tenderAmount: number;
  currencyCode?: string;
}

interface changeResult {
  optimalChange: [];
  totalChange?: number;
  changeArrayString?: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public fetchedChangeData = false;
  public fetchedOptimalChange: changeResult = { optimalChange: [], totalChange: 0, changeArrayString: "Submit the form for result..." };

  constructor(private http: HttpClient) {}

  ngOnInit() {
    console.log("Started vending machine...");
  }


  // Format API response to data model
  processAPIResponse(changeArray: []) {
    const total = changeArray.reduce((a, b) => a + b, 0);
    this.fetchedOptimalChange = {
      optimalChange: changeArray,
      totalChange: total,
      changeArrayString: changeArray.toString()
    }
  }

  // Fetch optimal change from API using default currency 
  getOptimalChangeDefaultCurrency(formInputs: purchaseFormData) {
    let params = new HttpParams();
    Object.keys(formInputs).forEach(key => {
      params = params.append(key, (formInputs as any)[key]);
    });
    const requestOptions = {
      params,
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Cache-control': 'no-cache'
      })
    };
 

    this.http.get<[]>('/Change/calculate-change',requestOptions).subscribe(
      (result) => {
        this.processAPIResponse(result);
      },
      (error) => {
        console.error(error);
      }
    );

  }

  // Fetch optimal change from API using user selected currency 
  getOptimalChangeSelectedCurrency(formInputs: purchaseFormData) {
    let params = new HttpParams();
    Object.keys(formInputs).forEach(key => {
      params = params.append(key, (formInputs as any)[key]);
    });
    const requestOptions = {
      params,
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Cache-control': 'no-cache'
      })
    };


    this.http.get<[]>('/Change/calculate-change-with-currency', requestOptions).subscribe(
      (result) => {
        this.processAPIResponse(result);
      },
      (error) => {
        console.error(error);
      }
    );

  }

  // Format and handle the submission of the form
  processFormSubmission(purchaseAmount: string, tenderAmount: string, currencyCode: string) {
    const formattedSubmission: purchaseFormData = {
      purchaseAmount: parseFloat(purchaseAmount),
      tenderAmount: parseFloat(tenderAmount),
      currencyCode: currencyCode
    };

    if (currencyCode == "NA") {
      this.getOptimalChangeDefaultCurrency(formattedSubmission);
    } else {
      this.getOptimalChangeSelectedCurrency(formattedSubmission);
    }
  }

  title = 'omnisienttest.client';
}
