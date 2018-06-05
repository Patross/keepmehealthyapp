using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System;

namespace KeepMeHealthy
{
    [Activity(Label = "KeepMeHealthy", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            //SELECTING SPINNERS
            Spinner spinnerWeight = FindViewById<Spinner>(Resource.Id.spinnerWeight);
            Spinner spinnerDistance = FindViewById<Spinner>(Resource.Id.spinnerDistance);

            //ADDING VALUES TO SPINNERS;
            List<string> listWeight = new List<string>();
            listWeight.Add("KG");
            listWeight.Add("POUNDS");

            List<string> listDistance = new List<string>();
            listDistance.Add("KM");
            listDistance.Add("MILES");

            ArrayAdapter adapterWeight = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, listWeight);
            ArrayAdapter adapterDistance = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, listDistance);

            spinnerWeight.Adapter = adapterWeight;
            spinnerDistance.Adapter = adapterDistance;


            //GETTING THE CALCULATE BUTTON
            Button btnCalculate = FindViewById<Button>(Resource.Id.btnCalculate);
            btnCalculate.Click += BtnCalculate_Click;

            //NOW THE PROFILE BUTTON
            ImageButton btnProfile = FindViewById<ImageButton>(Resource.Id.btnProfile);
            btnProfile.Click += BtnProfile_Click;



        }

        private void BtnProfile_Click(object sender, EventArgs e)
        {
            this.SetContentView(Resource.Layout.Profile);
        }

        private void BtnCalculate_Click(object sender, System.EventArgs e)
        {
            TextView txtCaloriesBurned = FindViewById<TextView>(Resource.Id.txtCaloriesBurned);

            EditText inputWeight = FindViewById<EditText>(Resource.Id.inputWeight);
            EditText inputDistanceTravelled = FindViewById<EditText>(Resource.Id.inputDistanceTravelled);
            EditText inputTime = FindViewById<EditText>(Resource.Id.inputTime);

            if (decimal.TryParse(inputWeight.Text, out decimal weight))
            {
                if (decimal.TryParse(inputDistanceTravelled.Text, out decimal distanceTravelled))
                {
                    if (decimal.TryParse(inputTime.Text, out decimal time))
                    {
                        //EVERYTHING SUCCESSFULLY PARSED IN FROM THE EDIT TEXT FIELDS

                        //DO THE CONVERTING
                        Spinner spinnerWeight = FindViewById<Spinner>(Resource.Id.spinnerWeight);
                        Spinner spinnerDistance = FindViewById<Spinner>(Resource.Id.spinnerDistance);

                        const decimal POUNDTOKG = 0.453592m;
                        const decimal MILESTOKM = 1.609m;


                        if (spinnerWeight.GetItemAtPosition(0).ToString() == "POUNDS")
                        {
                            weight = weight * POUNDTOKG;
                        }
                        if (spinnerDistance.GetItemAtPosition(0).ToString() == "MILES")
                        {
                            distanceTravelled = distanceTravelled * MILESTOKM;
                        }


                        RadioButton radioExerciseRunning = FindViewById<RadioButton>(Resource.Id.radExerciseRunning);
                        RadioButton radioExerciseWalking = FindViewById<RadioButton>(Resource.Id.radExerciseWalking);
                        decimal temp = ((Func<decimal>)(() =>
                        {

                            if (radioExerciseRunning.Selected)
                            {
                                return 8.5m;
                            }
                            else
                            {
                                return 3.3m;
                            }

                        }))();

                        decimal mets = temp;

                        decimal caloriesBurned = (weight * mets) * time;
                        txtCaloriesBurned.Text = $"You have burned {caloriesBurned} calories by exercising for {time} minutes and travelled {distanceTravelled} KM.";
                    }
                }
            }
        }
    }
}

