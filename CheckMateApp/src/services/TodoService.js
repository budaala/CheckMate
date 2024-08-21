
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

const getTodoById = async (id) => {
  try {
    const response = await $http.get(`get/${id}`);
    if (response.status === 200) {
      return response.data;
    }
    else {
      throw new Error(`Failed to fetch todo: ${response.statusText}`);
    }
  }
  catch (error) {
    console.error('Error fetching todo:', error);
    throw error;
  }
}

const createNewTodo = async (todo) => {
  try {
    const response = await $http.post('add', todo);
    if (response.status === 200) { 
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
    console.log('Updating todo: ', id,  updatedTodo);
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

const deleteTodo = async (id) => {
  try {
    const response = await $http.delete(`delete/${id}`);
    if (response.status === 200) {
      console.log('Todo deleted successfully:', id);
      return response.data;
    }
    else {
      throw new Error(`Failed to delete todo: ${response.statusText}`);
    }
  }
  catch (error) {
    console.error('Error deleting todo:', error);
    throw error;
  }
}

export default {
  getAll,
  createNewTodo,
  updateTodo,
  getTodoById,
  deleteTodo
};