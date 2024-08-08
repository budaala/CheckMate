<template>
  <v-card-text>
    <v-list>
      <v-list-item id="Todo-list-item" v-for="todo in list" :key="todo.id">
        <todo-component
          :todo="todo"
          @update:completed="updateTodoCompleted"
        ></todo-component>
      </v-list-item>
    </v-list>
  </v-card-text>
</template>

<script>
import todoComponent from "@/components/Todo.vue";

export default {
  name: "TodoList",
  components: {
    todoComponent,
  },
  props: {
    list: {
      type: Array,
      required: true,
    },
  },
  methods: {
    updateTodoCompleted(id, completed) {
      const todo = this.list.find((todo) => todo.id === id);
      if (todo) {
        todo.completed = completed;
        console.log(todo.completed);
      }
    },
  },
};
</script>

<style scoped>
#Todo-list-item {
  margin-bottom: 1em;
  border-bottom: 1px solid #e0e0e0;
}

.v-list-item:last-child {
  border-bottom: none;
}
</style>
