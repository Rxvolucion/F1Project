<template>
  <div>
    <h2>Driver Details</h2>
    <!-- Pass driverId as a prop to the F1Driver component -->
    <F1Driver :driverId="$route.params.driverId"/>
  </div>
</template>

<script>
import F1Driver from '../components/F1Driver.vue';  // Import the component

export default {
  name: "DriverDetailView",
  components: {
    F1Driver  // Register the component
  }
};
</script>

