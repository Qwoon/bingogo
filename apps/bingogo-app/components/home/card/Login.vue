<script setup lang="ts">
let showPassword = ref(false)
const isLoading = ref<boolean>(false)

let { value: email, handleChange: emailChange } = useField<string>('email')
let { value: password, handleChange: passwordChange } = useField<string>('password')

const onSubmit = () => {}
async function onReset() {}
</script>
<template>
  <div class="auth-wrapper d-flex align-center justify-center pa-4 h-100">
    <VCard class="auth-card pa-4 pt-7" variant="text">
      <VCardText class="pt-2">
        <h5 class="text-h5 font-weight-semibold mb-1">Login</h5>
        <p class="mb-0">Please sign-in to your account.</p>
      </VCardText>

      <VCardText class="pt-2">
        <VForm @submit.prevent="onSubmit">
          <VRow>
            <VCol cols="12">
              <VTextField
                v-model="email"
                @change="emailChange"
                label="Email"
                prepend-inner-icon="mdi-account-circle-outline"
              />
            </VCol>

            <VCol cols="12">
              <VTextField
                v-model="password"
                :type="showPassword ? 'text' : 'password'"
                @click:append-inner="showPassword = !showPassword"
                label="Password"
                prepend-inner-icon="mdi-lock-outline"
                @update:model-value="passwordChange"
                :append-inner-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
              />
            </VCol>

            <VCol cols="12">
              <span
                style="
                  text-decoration: underline;
                  color: rgb(var(--v-theme-primary));
                  cursor: pointer;
                "
                @click="onReset"
              >
                Forgot your password?
              </span>
            </VCol>

            <VCol cols="12">
              <VBtn block size="large" type="submit" variant="flat" :readonly="isLoading">
                <template v-if="isLoading">
                  <VProgressCircular indeterminate size="20" color="white" />
                </template>
                <template v-else> Login </template>
              </VBtn>
            </VCol>
          </VRow>
        </VForm>
      </VCardText>
    </VCard>
  </div>
</template>
