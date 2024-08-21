<template>
  <v-card-text>
    <v-list>
        <v-list-item class="todo-list-item" v-for="todo in list" :key="todo.id">
          <todo-component
            :todo="todo"
            @update:is-completed="
              $emit(
                'update:is-completed',
                todo.id,
                { ...todo, isCompleted: $event }
              )
            "
            @update:show-details="$emit('update:show-details', todo)"
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
};
</script>

<style scoped>
.todo-list-item {
  margin-bottom: 1em;
  /* border-bottom: 1px solid #e0e0e0; */
}

.v-list-item:last-child {
  border-bottom: none;
}
</style>
