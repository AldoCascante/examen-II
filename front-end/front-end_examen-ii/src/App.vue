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
      <div class="drink-info" v-for="(info, i) in drinksAvailable.information" :key="i" :value="i">
      <div> {{ info.name }} </div>
      <div> Cantidad disponible {{ info.available }} </div>
      <div> Precio: ₡{{ info.price }} </div>
      <button class="btn" @click="buy(info.name, info.price)"> Comprar </button>
      </div>
    </div>

    <div class="button-display">
      <div class="display-window" v-show="!OutOfOrder.value">
      <div v-show="!infoLocal.addingDrink"> Total de refrescos: {{ infoLocal.drinksTotal }} </div>
      <div v-show="!infoLocal.addingDrink"> Costo total: {{ infoLocal.costTotal }} </div>
      <div v-show="!infoLocal.addingDrink"> Vuelto: {{ infoLocal.cashAvailable - infoLocal.costTotal }}</div>
      <div v-show="infoLocal.addingDrink"> Cantidad: {{ infoLocal.newQuantity }} </div>
      </div>
      <div class="display-window" v-show="OutOfOrder.value"> {{ ErrorMessage }} </div>
      
      <div>
      <button class="btn" @click="accept" style="display: inline-block; width: 30%;"> Aceptar </button>
      <button class="btn" @click="cancel" style="display: inline-block; width: 30%;"> Cancelar </button>
      </div>

      <div>
      <div class="drink-info" style="">Cambiar cantidad </div>
      <button class="btn" @click="plus" style="background-color: #2C2C2C; display: inline-block; width: 15%;"> + </button>
      <button class="btn" @click="minus" style="background-color: #2C2C2C; display: inline-block; width: 15%;"> - </button>
      </div>
    </div>
  </div>
  
  </div>
</template>

<script setup>
import { reactive, ref, onMounted } from 'vue'
import axios from 'axios'
var OutOfOrder = ref(0)
var ErrorMessage = ref("")
var transactionInfo = reactive(
  {
    thousandBills: 0,
    fiveHundredCoins: 0,
    oneHundredCoins: 0,
    fiftyCoins: 0,
    twentyFiveCoins: 0,
    drinks: [],
  }
) 
var infoLocal = reactive(
  {
    costTotal: 0,
    cashAvailable: 0,
    drinksTotal: 0,
    addingDrink: 0,
    newDrink: '',
    newQuantity: 1,
    newPrice: 1,
  }
) 
var drinksAvailable = reactive( {information: [
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
] })

function loadCash(amount) {
  infoLocal.cashAvailable = infoLocal.cashAvailable + amount;

  if (amount === 1000) {
    transactionInfo.thousandBills = transactionInfo.thousandBills + 1;
  }

  if (amount === 500) {
    transactionInfo.fiveHundredCoins = transactionInfo.fiveHundredCoins + 1;
  }

  if (amount === 100) {
    transactionInfo.oneHundredCoins = transactionInfo.oneHundredCoins + 1;
  }

  if (amount === 50) {
    transactionInfo.fiftyCoins = transactionInfo.fiftyCoins + 1;
  }

  if (amount === 25) {
    transactionInfo.twentyFiveCoins = transactionInfo.twentyFiveCoins + 1;
  }
}

function addDrink(name, quantity, price) {
  infoLocal.costTotal = infoLocal.costTotal + (quantity*price);
  infoLocal.drinksTotal = infoLocal.drinksTotal + quantity;
  transactionInfo.drinks.push(name, quantity);
}

function cancel() {
  if (infoLocal.addingDrink === 1) {
    infoLocal.addingDrink = 0;
    infoLocal.newQuantity = 1;
  } else {
    infoLocal.cashAvailable = 0;
    infoLocal.costTotal = 0;
    infoLocal.addingDrink = 0;
    infoLocal.drinksTotal
  }
}

function accept() {
  if (infoLocal.addingDrink === 1) {
    addDrink(infoLocal.newDrink, infoLocal.newQuantity, infoLocal.newPrice);
    infoLocal.addingDrink = 0;
    infoLocal.newQuantity = 1;
  } else {
    if (infoLocal.drinksTotal > 0) {
      BuyDrinks();
      infoLocal.cashAvailable = 0;
      infoLocal.costTotal = 0;
      infoLocal.drinksTotal = 0;
    }
  }
}

function plus() {
  if (infoLocal.addingDrink === 1) {
    infoLocal.newQuantity = infoLocal.newQuantity + 1;
  }
}

function minus() {
  if (infoLocal.addingDrink === 1) {
    infoLocal.newQuantity = infoLocal.newQuantity - 1;
  }

  if (infoLocal.newQuantity < 1) {
    infoLocal.newQuantity = 1;
  }
}

function buy(name, price) {
  infoLocal.addingDrink = 1;
  infoLocal.newDrink = name;
  infoLocal.newPrice = price;
  infoLocal.newQuantity = 1;
}

function updateDrinksInfo() {
  try {
    axios.get(`https://localhost:7180/api/VedingMachine/GetAvailableDrinks`)
    .then((response) => {
      drinksAvailable.information = response.data;
    });

  } catch (error) {
    OutOfOrder.value = 1;
  }
}

function BuyDrinks() {
  
  const request = {
    thousandBills: transactionInfo.thousandBills,
    fiveHundredCoins: transactionInfo.fiveHundredCoins,
    oneHundredCoins: transactionInfo.oneHundredCoins,
    fiftyCoins: transactionInfo.fiftyCoins,
    twentyFiveCoins: transactionInfo.twentyFiveCoins,
    drinkOrders: []
  }

  transactionInfo.drinks.forEach((element) => request.drinkOrders.push(element.toString()));
  console.log(request);
  try {
    axios.post(`https://localhost:7180/api/VedingMachine/BuyDrinks`, request ).then(() => {
     window.location.href = "/";
    })
  } catch (error) {
    OutOfOrder.value = 1;
  }

  transactionInfo.thousandBills = 0,
  transactionInfo.fiveHundredCoins = 0,
  transactionInfo.oneHundredCoins = 0,
  transactionInfo.fiftyCoins = 0,
  transactionInfo.twentyFiveCoins = 0,
  transactionInfo.drinks = [];
}

onMounted(() => {
  updateDrinksInfo()
})

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
  text-align: left;
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

.button-display {
  display: inline-block;
  width: 50%;
  text-align: center;
}
</style>
