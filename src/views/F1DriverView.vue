<template>
  <div>
    <h1>Formula 1 Drivers</h1>

    <!-- Search Input -->
    <input
      v-model="searchTerm"
      type="text"
      placeholder="Search for a driver"
    />
    <button @click="searchDrivers">Search</button>

    <!-- Drivers List -->
    <ul>
      <li v-for="f1Driver in f1Drivers" :key="f1Driver.driverId">
        <router-link :to="'/f1drivers/' + f1Driver.driverId">
          {{ f1Driver.name }}
        </router-link>
        <img v-if="this.favoriteDriver === f1Driver.driverId" :src="`/img/favoriteicon.png`" width="32" />
        <p>Popularity Score: {{f1Driver.driverPopularity }}</p>
        <!-- <p v-if="this.favoriteDriver === f1Driver.driverId">This is my favorite driver</p>  -->
      </li>
    </ul>

    <!-- No Results Message -->
    <div v-if="f1Drivers.length === 0 && !searchTerm.trim()">
      <p>Loading drivers...</p>
    </div>
    <div v-else-if="f1Drivers.length === 0 && searchTerm.trim()">
      <p>No drivers match your search.</p>
    </div>
  </div>
</template>

---

### Script Section:

```vue
<script>
import F1DriverService from "../services/F1DriverService";

export default {
  name: "F1DriverList",

  data() {
    return {
      f1Drivers: [], // List of F1 drivers
      searchTerm: "", // User's input for search
      favoriteDriver: 0
    };
  },
  methods: {
    // Fetch all drivers
    getDrivers() {
      F1DriverService.getF1Drivers()
        .then((response) => {
          this.f1Drivers = response.data;
        })
        .catch((error) => this.handleError(error));
    },
    // Search drivers by name
    searchDrivers() {
      if (!this.searchTerm.trim()) {
        // If search term is empty, fetch all drivers
        this.getDrivers();
        return;
      }

      F1DriverService.getF1F1DriverByLikeName(this.searchTerm)
        .then((response) => {
          this.f1Drivers = response.data;
        })
        .catch((error) => this.handleError(error));
    },

    getFavoriteDriver() {
      const username = this.$store.state.user.username;

      F1DriverService.getUsersFavoriteDriver(username)
      .then((response) => {
        this.favoriteDriver = response.data
        console.log("favorite driver", this.favoriteDriver)
        
      })
      .catch((error) => {
        if (error.response) {
          console.log("Error getting driver: ", error.response.status);
        }
        else if (error.request) {
          console.log("Error getting driver: unable to communicate to server");
        }
        else {
          console.log("Error getting driver status: error making request");
        }
      })
    },


    // Handle request errors
    handleError(error) {
      if (error.response) {
        console.error(
          `Error fetching drivers: ${error.response.status} - ${error.response.statusText}`
        );
      } else if (error.request) {
        console.error("Error fetching drivers: No response from server.");
      } else {
        console.error("Error fetching drivers: Request could not be made.");
      }
    },
  },
  created() {
    this.getDrivers(); // Load drivers when the component is created
    this.getFavoriteDriver();
  },
};
</script>
