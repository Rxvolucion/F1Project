<template>
  <div v-if="f1Driver">
    <h1>{{ f1Driver.name }}</h1>
    <img :src="getFlagImage(f1Driver.nationality)" :alt="f1Driver.nationality + 'flag'" width="80" />
    <!-- <p><strong>Driver ID:</strong> {{ f1Driver.driverId }}</p> -->
    <p><strong>Popularity Score:</strong> {{ f1Driver.driverPopularity }}</p> 
    <p><strong>Team:</strong> {{ f1Driver.team }}</p>
    <p><strong>Date-of-Birth:</strong> {{ f1Driver.dob }}</p>
    <p><strong>Car Number:</strong> {{ f1Driver.carNum }}</p>
    <p><strong>Nationality:</strong> {{ f1Driver.nationality }}</p>
    <p><strong>World Championships:</strong> {{ f1Driver.worldChampionships }}</p>
    <p><strong>Number of GP Starts:</strong> {{ f1Driver.numberOfGPStarts }}</p>
    <p><strong>Number of Wins:</strong> {{ f1Driver.numberOfWins }}</p>
    <p><strong>Number of Podiums:</strong> {{ f1Driver.numberOfGPPodiums }}</p>
    <p><strong>Number of DNFs:</strong> {{ f1Driver.numberOfDNFs }}</p>

    <button @click="toggleFavorite">
      {{ isFavorite ? "Remove as Favorite" : "Add as Favorite" }}
    </button>

  </div>
</template>

<script>
import F1DriverService from '../services/F1DriverService';

export default {
  name: "F1Driver",

  data() {
    return {
      f1Driver: null,
      isFavorite: false,
      favoriteDriver: 0, 
    };
  },

  methods: {
    getDriverDetails() {
      const driverId = this.$route.params.driverId; // Get the driverId from the route parameters

      F1DriverService
        .getF1DriverById(driverId)  // Call the API with the driverId
        .then((response) => {
          this.f1Driver = response.data;
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error getting driver: ", error.response.status);
          } else if (error.request) {
            console.log("Error getting driver: unable to communicate to server");
          } else {
            console.log("Error getting driver status: error making request");
          }
        });
    },

    toggleFavorite() {
      const driverId = this.$route.params.driverId;
      const username = this.$store.state.user.username;
      
      if (this.isFavorite) {
        F1DriverService.undoAddOnePopularityToDriver(driverId)
        .then(() => {
          this.isFavorite=false;
          F1DriverService.unselectFavoriteDriver(username);
          
        })
        .catch((error) => {
          console.log("Error loading")
        })
      }
      else if (!this.isFavorite && this.favoriteDriver !== 0) {
        F1DriverService.undoAddOnePopularityToDriver(this.favoriteDriver)
        .then(() => {
          this.isFavorite=true;
          F1DriverService.addOnePopularityToDriver(driverId)
          F1DriverService.makeFavoriteDriver(driverId, username)
          
        })
        .catch((error) => {
          console.log("Error loading")
        })
      }
      else {
        F1DriverService.addOnePopularityToDriver(driverId)
        .then(() => {
          this.isFavorite=true;
          F1DriverService.makeFavoriteDriver(driverId, username)
          
        })
        .catch((error) => {
          console.log("error loading")
        })
      }
    },


    checkForFavoriteDriver() {
      const username = this.$store.state.user.username;

      F1DriverService.getUsersFavoriteDriver(username)
      .then((response) => {
        this.favoriteDriver = response.data
        console.log("before")
        console.log("favorite driver test f1Driver.driverId", this.$route.params.driverId)
        if (this.favoriteDriver != this.$route.params.driverId) {
          this.isFavorite=false;
          console.log("reached if statement")
        }
        else{
          this.isFavorite=true;
          console.log("reached else statement")
        }
      })
      .catch((error) => {
        if (error.response) {
          console.log("Error getting driver: ", error.response.status);
        }
        else if (error.request) {
          console.log("Error getting driver: unable to communicate to server");
        }
        else {
          console.log("Error getting driver status: error making request", error.message);
        }
      })
    },


    getFlagImage(nationality) {
      return `/img/${nationality.toLowerCase() + 'flag'}.png`
    },
  },

  created() {
    this.getDriverDetails();
    this.checkForFavoriteDriver();
  }
};
</script>