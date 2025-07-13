<template>
  <div class="vending-machine">

  <div class="cash-input">  
    <div class="display-window">
      Monto ingresado: {{ infoLocal.cashAvailable }}
    </div>

    <div class="cash-select">
      <div>₡1000</div>
      <button class="btn" @click="loadCash(1000)"> Insertar </button>
    </div>
    <div class="cash-select">
      <div>₡500</div>
      <button class="btn" @click="loadCash(500)"> Insertar </button>
    </div>
    <div class="cash-select">
      <div>₡100</div>
      <button class="btn" @click="loadCash(100)"> Insertar </button>
    </div>
    <div class="cash-select">
      <div>₡50</div>
      <button class="btn" @click="loadCash(50)"> Insertar </button>
    </div>
    <div class="cash-select">
      <div>₡25</div>
      <button class="btn" @click="loadCash(25)"> Insertar </button>
    </div>

  </div>

  <div class="drinks-input">
    <div class="drinks-display">
      <div class="drink-info" v-for="(info, i) in drinksAvailable" :key="i" :value="i">
      <div> {{ info.name }} </div>
      <div> Cantidad disponible {{ info.available }} </div>
      <div> Precio: ₡{{ info.price }} </div>
      <button class="btn" @click="loadCash(25)"> Comprar </button>
      </div>
    </div>

    <div class="button-display" style="display: inline-block; width: 50%;">
      <div class="display-window">
      <div v-show="1"> Total de refrescos: </div>
      <div v-show="1"> Costo total: {{ infoLocal.costoTotal }}</div>
      <div v-show="1"> Vuelto: </div>
      </div>
      <button class="btn" @click="addDrink('drink1', 100, 5)"> Comprar </button>
    </div>
  </div>
  
  </div>
</template>

<script setup>
import { reactive } from 'vue'
//import axios from 'axios'
var transactionInfo = reactive(
  {
    thousandBills: 0,
    fiveHundredCoins: 0,
    oneHundredCoins: 0,
    fiftyCoins: 0,
    twentyFiveCoins: 0,
    drinks: []
  }
) 
var infoLocal = reactive(
  {
    costoTotal: 0,
    cashAvailable: 0,
  }
) 
var drinksAvailable = reactive( [
  {
    name: 'DrinkTemplate1',
    available: 5,
    price: 100
  },
  {
    name: 'DrinkTemplate2',
    available: 6,
    price: 200
  }
] )

function loadCash(amount) {
  infoLocal.cashAvailable = infoLocal.cashAvailable + amount;

  if (amount === 1000) {
    transactionInfo.thousandBills = transactionInfo.thousandBills + 1
  }

  if (amount === 500) {
    transactionInfo.fiveHundredCoins = transactionInfo.fiveHundredCoins + 1
  }

  if (amount === 100) {
    transactionInfo.oneHundredCoins = transactionInfo.oneHundredCoins + 1
  }

  if (amount === 50) {
    transactionInfo.fiftyCoins = transactionInfo.fiftyCoins + 1
  }

  if (amount === 25) {
    transactionInfo.twentyFiveCoins = transactionInfo.twentyFiveCoins + 1
  }

  console.log(transactionInfo);
}

function addDrink(name, price, quantity) {
  transactionInfo.drinks.push(name, price*quantity);
  console.log(transactionInfo.drinks);
}


</script>

<style>
.vending-machine {
  margin: 5rem;
}

.cash-input {
  display: inline-block;
  width: 30%;
  vertical-align: top
}

.drinks-input {
  display: inline-block;
  width: 70%;
  vertical-align: top
}

.display-window {
  margin-left: 2rem;
  margin-right: 2rem;
  margin-bottom: 2rem;
  padding: 1rem;
  background-color: black;
  color: white;
  font-family: Verdana;
}

.cash-select {
  text-align: center;
  color: #000000;
  font-family: Verdana;
  font-size: 25px;
  vertical-align: top
}

.drinks-display {
  display: inline-block;
  width: 50%;
  text-align: center;
  font-family: Verdana;
  vertical-align: top
}

.drink-info {
  text-align: center;
  color: #000000;
  font-family: Verdana;
  font-size: 20px;
}

.btn {
  background-color: #14AE5C;
  color: white;
  border: 1px solid #000000;
  padding: 0.5rem 1rem;
  border-radius: 6px;
  cursor: pointer;
  margin: 1rem;
  font-size: 20px;
}
</style>
