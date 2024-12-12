import axios from 'axios';

export default {

  getF1Drivers() {
    return axios.get('f1driver')
  },

  getF1DriverById(driverId) {
    return axios.get(`/f1driver/${driverId}`)
  },
  
  getF1F1DriverByLikeName(name) {
    return axios.post(`f1driver/searchname?name=${name}`)
  },

  addOnePopularityToDriver(driverId) {
    return axios.put(`f1driver/favorite/${driverId}`)
  },

  undoAddOnePopularityToDriver(driverId) {
    return axios.put(`f1driver/unfavorite/${driverId}`)
  },

  // makeFavoriteDriver(driverId, username) {
  //   return axios.put(`user/${driverId}`, {username})
  // },
  makeFavoriteDriver(driverId, username) {
    return axios.put(`user/${driverId}/${username}`)
  },

  unselectFavoriteDriver(username) {
    return axios.put(`user/unfavorite/${username}`)
  },

  getUsersFavoriteDriver(username) {
    return axios.get(`user/favoritedriver/${username}`)
  }

}
