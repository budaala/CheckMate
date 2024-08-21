<template>
  <v-card variant="tonal" color="primary">
    <!-- Loading state -->
    <div v-if="loading">Loading...</div>
    <!-- Main content -->
    <!-- Displaying details -->
    <div v-else>
      <div v-if="!isEditing">
        <v-card-subtitle>
          <p class="mt-5">{{ checkCompleted(localTodo) }}</p>
          <v-sheet class="d-flex align-center" rounded="lg">
            <v-progress-linear
              :location="null"
              buffer-opacity="1"
              buffer-value="10"
              color="primary"
              height="12"
              max="10"
              min="0"
              :model-value="localTodo.isCompleted ? 10 : 0"
              rounded
            ></v-progress-linear>
          </v-sheet>
        </v-card-subtitle>
        <v-card-title>
          <h2>{{ localTodo.title }}</h2>
        </v-card-title>
        <v-card-subtitle v-if="localTodo.subtitle">
          <h3>{{ localTodo.subtitle }}</h3>
        </v-card-subtitle>
        <v-card-text>
          <v-row class="crud-dates">
            <v-col>
              <h3>Created at:</h3>
              <p>{{ formatDate(localTodo.createdAt) }}</p>
            </v-col>
            <v-col v-if="localTodo.isCompleted">
              <h3>Completed at:</h3>
              <p>{{ formatDate(localTodo.completedDate) }}</p>
            </v-col>
            <!-- <v-col v-if="localTodo.updatedAt">
                        <h3>Updated at:</h3>
                        <p>{{ formatDate(localTodo.updatedAt) }}</p>
                    </v-col> -->
          </v-row>
        </v-card-text>
      </div>
      <!-- Editing state -->
      <v-card-text v-else>
        <v-text-field v-model="localTodo.title" label="Title"></v-text-field>
        <v-text-field
          v-model="localTodo.subtitle"
          label="Subtitle"
        ></v-text-field>
        <v-checkbox
          v-model="localTodo.isCompleted"
          :label="checkCompleted(localTodo)"
          color="primary"
        >
        </v-checkbox>
      </v-card-text>
    </div>
    <!-- Actions -->
    <v-card-actions class="mt-5">
      <div v-if="!isEditing">
        <v-btn
          prepend-icon="mdi-pencil"
          variant="outlined"
          color="primary"
          @click="startEditing"
          >Edit</v-btn
        >
      </div>
      <div v-else>
        <v-btn
          prepend-icon="mdi-content-save"
          variant="outlined"
          color="primary"
          @click="saveTodo"
          >Save</v-btn
        >
        <v-btn
          prepend-icon="mdi-cancel"
          variant="outlined"
          color="error"
          @click="isEditing = false"
        >
          Cancel
        </v-btn>
      </div>
      <v-btn
        class="ml-2"
        prepend-icon="mdi-delete"
        variant="outlined"
        color="error"
        @click="isDeleting = true"
        >Delete</v-btn
      >
    </v-card-actions>
  </v-card>
  <!-- Delete dialog -->
  <v-dialog v-model="isDeleting" max-width="500">
    <v-card>
      <v-card-title>Confirm delete</v-card-title>
      <v-card-text>Are you sure you want to delete this todo?</v-card-text>
      <v-card-actions>
        <v-btn color="error" text @click="confirmDelete">Yes</v-btn>
        <v-btn text @click="isDeleting = false">No</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import TodoService from "@/services/TodoService";
export default {
  props: {
    todo: {
      type: Object,
      required: true,
    },
  },
  data() {
    return {
      localTodo: { ...this.todo },
      isEditing: false,
      isDeleting: false,
      loading: false,
    };
  },
  emits: ["action:edit-todo", "action:delete-todo"],
  watch: {
    todo: {
        handler(newTodo) {
        this.localTodo = { ...newTodo };
      },
      deep: true,
      immediate: true,
    },
  },
  methods: {
    checkCompleted(todo) {
      return todo.isCompleted == true ? "Completed" : "Not completed";
    },
    startEditing() {
      this.isEditing = true;
    },
    async saveTodo() {
      this.$emit("action:edit-todo", this.todo.id, this.localTodo);
      this.isEditing = false;
    },
    confirmDelete() {
      this.$emit("action:delete-todo", this.todo.id);
    },
    formatDate(date) {
      if (!date) {
        return "Invalid date";
      }
      const parsedDate = new Date(date);
      if (isNaN(parsedDate.getTime())) {
        return "Invalid date";
      }
      return parsedDate.toLocaleString();
    },
  },
};
</script>

<style scoped>
.crud-dates {
  justify-content: space-between;
  display: flex;
  flex-direction: row;
}

@media screen and (max-width: 1400px) {
  .crud-dates {
    flex-direction: column !important;
  }
}
</style>
