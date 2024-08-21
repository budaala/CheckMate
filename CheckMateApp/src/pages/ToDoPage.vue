<template>
  <v-container v-if="!errorFetchingData">
    <v-row justify="center">
      <v-col cols="6">
        <v-card>
          <v-card-title>
            <h2>{{ list.title }}</h2>
          </v-card-title>
          <v-card-text>
            <p>{{ list.description }}</p>
          </v-card-text>
          <v-card-text id="add-Todo-card">
            <v-form class="w-100" align="center">
              <v-row class="mb-2">
                <v-expansion-panels>
                  <v-expansion-panel>
                    <v-text-field
                      clearable
                      label="Add new todo"
                      variant="underlined"
                      v-model="newTodoTitle"
                      :error-messages="errorMessages"
                      @input="errorMessages = []"
                      required
                    ></v-text-field>
                    <v-expansion-panel-title>
                      More options
                    </v-expansion-panel-title>
                    <v-expansion-panel-text>
                      <v-text-field
                        label="Add subtitle"
                        variant="underlined"
                        id="subtitle-text-field"
                        v-model="newTodoSubtitle"
                      >
                      </v-text-field>
                    </v-expansion-panel-text>
                  </v-expansion-panel>
                </v-expansion-panels>
              </v-row>
              <v-row justify="end">
                <v-btn prepend-icon="mdi-plus" @click="addTodo">Add</v-btn>
              </v-row>
            </v-form>
          </v-card-text>
          <todoList
            :list="todos"
            @update:is-completed="handleTodoUpdate"
            @update:show-details="toggleTodoDetails"
          ></todoList>
        </v-card>
      </v-col>
      <v-col v-if="clickedTodo">
        <todoDetails
          :todo="clickedTodo"
          @action:edit-todo="handleTodoUpdate"
          @action:delete-todo="deleteTodo"
        ></todoDetails>
      </v-col>
    </v-row>
  </v-container>
  <v-container v-else>
    <v-row justify="center">
      <v-col cols="6">
        <v-alert type="error"
          >Error fetching data. Try refreshing the page.</v-alert
        >
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { ref } from "vue";
import todoList from "@/components/TodoList.vue";
import TodoService from "@/services/TodoService";
import todoDetails from "@/components/TodoDetails.vue";

export default {
  name: "ToDoPage",
  components: {
    todoList,
    todoDetails,
  },
  data: () => ({
    todos: [],
    list: {
      title: "My To-Do List",
      description: "This is a list of things I need to do.",
    },
    isExpanded: false,
    errorMessages: [],
    errorFetchingData: false,
    clickedTodo: null,
  }),
  setup() {
    const newTodoTitle = ref("");
    const newTodoSubtitle = ref("");
    return { newTodoTitle, newTodoSubtitle };
  },
  async mounted() {
    try {
      await this.fetchTodos();
    } catch (error) {
      console.error(error);
      this.errorFetchingData = true;
    }
  },
  methods: {
    async fetchTodos() {
      this.todos = await TodoService.getAll();
      this.sortTodos();
    },
    async addTodo() {
      if (!this.newTodoTitle) {
        this.errorMessages = ["Todo title is required"];
        return;
      }
      const newTodo = {
        title: this.newTodoTitle,
        isCompleted: false,
        subtitle: this.newTodoSubtitle,
      };
      await TodoService.createNewTodo(newTodo);
      await this.fetchTodos();
      this.newTodoTitle = "";
      this.newTodoSubtitle = "";
    },
    sortTodos() {
      this.todos.sort((a, b) => {
        return a.isCompleted - b.isCompleted;
      });
    },
    toggleMoreOptions() {
      this.isExpanded = !this.isExpanded;
    },
    toggleTodoDetails(todo) {
      if (this.clickedTodo && this.clickedTodo.id === todo.id) {
        this.clickedTodo = null;
      } else {
        this.clickedTodo = { ...todo };
      }
    },
    async handleTodoUpdate(id, updatedTodo) {
      // add completedDate to updatedTodo if isCompleted is true
      if (updatedTodo.isCompleted) {
        updatedTodo.completedDate = new Date().toISOString();
      }
      await TodoService.updateTodo(id, updatedTodo);
      await this.fetchTodos();
      if (this.clickedTodo && this.clickedTodo.id === id) {
        this.clickedTodo = { ...this.clickedTodo, ...updatedTodo };
      }
    },
    async deleteTodo() {

      await TodoService.deleteTodo(this.clickedTodo.id);
      await this.fetchTodos();
      this.clickedTodo = null;
    },
  },
};
</script>

<style scoped>
#add-Todo-card {
  display: flex;
  flex-direction: row;
  align-items: center;
  margin: 1em;
  padding: 1em 1em 0 1em;
}

.v-expansion-panel {
  padding: 0 1em 1em 1em;
}

.v-expansion-panel-title {
  min-width: 25%;
  margin: 0;
  padding: 0;
  font-size: 1em;
}

.v-expansion-panel-text {
  width: 100%;
  margin: 0;
  padding: 0;
}

.v-list {
  padding-top: 0;
  margin: 0;
}
</style>
