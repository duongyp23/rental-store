<template>
  <div class="style-input">
    <div v-if="label != null" class="input-label">{{ label }}</div>
    <div class="row-input" :class="{ disabled: disabled }">
      <div
        v-if="rightIcon != null"
        :class="'icon-input'"
        :style="'background-image : url(' + Images[rightIcon] + ')'"
      ></div>

      <input
        v-if="type == 'text'"
        type="text"
        class="input-box"
        :value="value"
        :placeholder="[placeholder ? placeholder : label ? label : '']"
        @input="$emit('update:value', $event.target.value)"
        @change="$emit('changeValue')"
        :disabled="disabled"
        :spellcheck="false"
        :maxlength="maxlength"
      />
      <textarea
        v-else-if="type == 'textarea'"
        class="input-box"
        :value="value"
        :placeholder="[placeholder ? placeholder : label ? label : '']"
        @input="$emit('update:value', $event.target.value)"
        @change="$emit('changeValue')"
        :disabled="disabled"
        :spellcheck="false"
      />
      <input
        v-else-if="type == 'date'"
        type="date"
        class="input-box"
        :value="value"
        :placeholder="[placeholder ? placeholder : label ? label : '']"
        @input="$emit('update:value', $event.target.value)"
        @change="$emit('changeValue')"
        :disabled="disabled"
      />
    </div>
  </div>
</template>
<script>
import Images from "@/assets/icon/images";
export default {
  data() {
    return {
      Images,
    };
  },
  props: {
    placeholder: { type: String }, // placeholder
    label: { type: String },
    type: { default: "text", type: String },
    value: { default: null, type: String }, // giá trị input
    rightIcon: { type: String }, // icon
    id: { default: null, type: String }, // ID
    disabled: { type: Boolean, default: false },
    maxlength: { type: Int32Array, default: 255 },
  },
};
</script>
<style>
@import url(./styleInput.scss);
</style>