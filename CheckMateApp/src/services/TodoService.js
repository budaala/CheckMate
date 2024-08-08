
import axios from 'axios';

const $http = axios.create({
  baseURL: 'http://localhost:5217/todo/', // Zastąp swoim URL API
  headers: {
    'Content-Type': 'application/json',
  },
});

const getAll = async () => {
  try {
    const response = await $http.get('getAll');
    if (response.status === 200) {
      console.log("All records: ");
      console.log(response.data);
      return response.data;
    }
  }
  catch (error) {
    console.error(error);
  }
}

const createNewTodo = async (todo) => {
  try {
    const response = await $http.post('add', todo);
    if (response.status === 200) {
      console.log("Added: " + response.data);
      return response.data;
    }
  }
  catch (error) {
    console.error(error);
  }
}

export default {
  getAll,
  createNewTodo,
};