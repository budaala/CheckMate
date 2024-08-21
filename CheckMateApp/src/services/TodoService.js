
import axios from 'axios';

const $http = axios.create({
  baseURL: 'http://localhost:5217/todo/', 
  headers: {
    'Content-Type': 'application/json',
  },
});

const getAll = async () => {
  try {
    const response = await $http.get('getAll');
    if (response.status === 200) {
      return response.data;
    }
    else {
      throw new Error('Failed to fetch data: ', response.statusText);
    }
  }
  catch (error) {
    console.error("Error fetching todos: ", error);
    throw error;
  }
}

const createNewTodo = async (todo) => {
  try {
    await $http.post('add', todo);
    if (response.status === 201) { // (201 Created)
      return response.data; 
    } else {
      throw new Error(`Failed to create todo: ${response.statusText}`);
    }
  }
  catch (error) {
    console.error('Error creating todo:', error);
    throw error;
  }
}

const updateTodo = async (id, updatedTodo) => {
  try {
    const response = await $http.put(`update/${id}`, updatedTodo);
    if (response.status === 200) {
      console.log('Todo updated successfully:', updatedTodo);
      return response.data;
    }
    else {
      throw new Error(`Failed to update todo: ${response.statusText}`);
    }
  }
  catch (error) {
    console.error('Error updating todo:', error);
    throw error;
  }
}

export default {
  getAll,
  createNewTodo,
  updateTodo
};