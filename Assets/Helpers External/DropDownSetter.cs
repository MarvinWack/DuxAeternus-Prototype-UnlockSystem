using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class DropDownSetter : MonoBehaviour {
    public List<string> countryOptions = new List<string>() {
        "-- Region: Africa --",
        "Algeria - DZ",
        "Egypt - EG",
        "Mauritius - MU",
        "Mayotte - YT",
        "Morocco - MA",
        "South Africa - ZA",
        "Tunisia - TN",
        "-- Region: North America --",
        "Canada - CA",
        "Costa Rica - CR",
        "Mexico - MX",
        "Panama - PA",
        "Puerto Rico - PR",
        "United States - US",
        "-- Region: South America --",
        "Argentina - AR",
        "Bolivia - BO",
        "Brazil - BR",
        "Chile - CL",
        "Colombia - CO",
        "Ecuador - EC",
        "Peru - PE",
        "Uruguay - UY",
        "Venezuela - VE",
        "-- Region: Asia --",
        "Armenia - AM",
        "Bahrain - BH",
        "Hong Kong - HK",
        "India - IN",
        "Indonesia - ID",
        "Israel - IL",
        "Japan - JP",
        "Kuwait - KW",
        "Lebanon - LB",
        "Malaysia - MY",
        "Maldives - MV",
        "Oman - OM",
        "Pakistan - PK",
        "Philippines - PH",
        "Qatar - QA",
        "Saudi Arabia - SA",
        "Singapore - SG",
        "South Korea - KR",
        "Taiwan - TW",
        "Thailand - TH",
        "Turkey - TR",
        "United Arab Emirates - AE",
        "Vietnam - VN",
        "-- Region: Europe --",
        "Albania - AL",
        "Austria - AT",
        "Belgium - BE",
        "Bosnia and Herzegovina - BA",
        "Bulgaria - BG",
        "Croatia - HR",
        "Cyprus - CY",
        "Czech Republic - CZ",
        "Denmark - DK",
        "Estonia - EE",
        "Finland - FI",
        "France - FR",
        "Georgia - GE",
        "Germany - DE",
        "Greece - GR",
        "Hungary - HU",
        "Iceland - IS",
        "Ireland - IE",
        "Italy - IT",
        "Kosovo - XK",
        "Latvia - LV",
        "Lithuania - LT",
        "Luxembourg - LU",
        "Macedonia - MK",
        "Malta - MT",
        "Montenegro - ME",
        "Netherlands - NL",
        "Norway - NO",
        "Poland - PL",
        "Portugal - PT",
        "Romania - RO",
        "Russia - RU",
        "Serbia - RS",
        "Slovakia - SK",
        "Slovenia - SI",
        "Spain - ES",
        "Sweden - SE",
        "Switzerland - CH",
        "Ukraine - UA",
        "United Kingdom - GB",
        "-- Region: Oceania --",
        "Australia - AU",
        "New Caledonia - NC",
        "New Zealand - NZ"
    };

    // Start is called before the first frame update
    void Start() {
        DropDownController dropDownController = GetComponent<DropDownController>();
        TMP_Dropdown dropdown = GetComponent<TMP_Dropdown>();

        dropdown.options.Clear();
        foreach(var item in countryOptions) {
            dropdown.options.Add(new TMP_Dropdown.OptionData(item));
        }

        dropDownController.EnableOption("-- Region: Africa --", false);
        dropDownController.EnableOption("-- Region: North America --", false);
        dropDownController.EnableOption("-- Region: South America --", false);
        dropDownController.EnableOption("-- Region: Asia --", false);
        dropDownController.EnableOption("-- Region: Europe --", false);
        dropDownController.EnableOption("-- Region: Oceania --", false);
    }
}
