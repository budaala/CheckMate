<template>
  <v-container>
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
          <todo-list :list="todos"></todo-list>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { ref } from "vue";
import todoList from "@/components/TodoList.vue";

export default {
  name: "ToDoPage",
  components: {
    todoList,
  },
  data: () => ({
    todos: [
      { id: 1, title: "Todo 1", completed: false, subtitle: "Subtitle 1" },
      { id: 2, title: "Todo 2", completed: false, subtitle: "Subtitle 2" },
      { id: 3, title: "Todo 3", completed: false, subtitle: "Subtitle 3" },
      { id: 4, title: "Todo 4", completed: false, subtitle: "Subtitle 4" },
      { id: 5, title: "Todo 5", completed: false, subtitle: "Subtitle 5" },
    ],
    list: {
      title: "My To-Do List",
      description: "This is a list of things I need to do.",
    },
    isExpanded: false,
    errorMessages: [],
  }),
  setup() {
    const newTodoTitle = ref("");
    const newTodoSubtitle = ref("");
    return { newTodoTitle, newTodoSubtitle };
  },
  methods: {
    async addTodo() {
      if (!this.newTodoTitle) {
        this.errorMessages = ["Todo title is required"];
        return;
      }
      this.todos.push({
        id: this.todos.length + 1,
        title: this.newTodoTitle,
        completed: false,
        subtitle: this.newTodoSubtitle,
      });
      this.newTodoTitle = "";
      this.newTodoSubtitle = "";
    },
    toggleMoreOptions() {
      this.isExpanded = !this.isExpanded;
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
  width: 25%;
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
