<template>
  <div>
    <h1 class="display-4 text-center mt-5">Listado de tareas</h1>
    <hr />
    <div class="row">
      <div class="col-lg-8 offset-lg-2">
        <div class="card mt-4">
          <div class="card-body">
            <div class="input-group">
              <input
                v-model="tarea"
                v-on:keyup.enter="agregarTarea()"
                type="text"
                class="form-control"
                placeholder="Agregar Tarea"
              />
              <button v-on:click="agregarTarea()" class="btn btn-success" type="button">
                Agregar
              </button>
            </div>
            <h5 v-if="listaTareas.length == 0" class="text-center mt-3">
              No hay tareas para realizar <span><i class="far fa-smile"></i></span>
            </h5>
            <ul class="list-group">
              <li
                v-for="tarea of listaTareas"
                :key="tarea.id"
                class="list-group-item d-flex justify-content-between"
              >
                <span
                  v-on:click="editarTarea(tarea, tarea.id)"
                  :class="{ 'text-primary': tarea.estado }"
                  class="cursor"
                >
                  <i
                    :class="[tarea.estado ? 'fas fa-check-circle' : 'far fa-circle']"
                  ></i>
                </span>
                {{ tarea.nombre }}
                <span v-on:click="eliminarTarea(tarea.id)" class="text-danger cursor">
                  <i class="fas fa-trash-alt"></i>
                </span>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Tarea",
  data() {
    return {
      tarea: "",
      listaTareas: [],
    };
  },
  methods: {
    agregarTarea() {
      const agregarTarea = async (tarea) => {
        let res = await fetch("http://localhost:32593/api/Tarea/", {
          method: "POST",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
          },
          body: JSON.stringify(tarea),
        });
        await res.json();
        this.obtenerTareas();
      };
      if (this.tarea.length > 0) {
        const tarea = {
          nombre: this.tarea,
          estado: false,
        };
        agregarTarea(tarea);
        this.tarea = "";
      }
    },
    eliminarTarea(index) {
      const eliminarTarea = async () => {
        let res = await fetch(`http://localhost:32593/api/Tarea/${index}`, {
          method: "DELETE",
        });
        await res.json();
        this.obtenerTareas();
      };
      eliminarTarea();
    },
    editarTarea(tarea, index) {
      const editarTarea = async (tarea, index) => {
        let res = await fetch(`http://localhost:32593/api/Tarea/${index}`, {
          method: "PUT",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
          },
          body: JSON.stringify(tarea),
        });
        await res.json();
        this.obtenerTareas();
      };
      editarTarea(tarea, index);
    },
    obtenerTareas() {
      const obtenerTareas = async () => {
        let res = await fetch("http://localhost:32593/api/Tarea/");
        this.listaTareas = await res.json();
      };
      obtenerTareas();
    },
  },

  created: function () {
    this.obtenerTareas();
  },
};
</script>

<style scoped>
.cursor {
  cursor: pointer;
}
ul li:first-child {
  margin-top: 2rem;
}
</style>
