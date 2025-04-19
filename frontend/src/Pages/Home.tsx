import React, { Component, JSX } from 'react'
import { Navigate } from 'react-router-dom'
import TokenHandler from '../Utilities/tokenHandler'
export class Home extends Component {
  tokenHandler: TokenHandler
  constructor(props: any) {
    super(props)
    this.tokenHandler = new TokenHandler()
    this.tokenHandler.Update()
  }
  render(): JSX.Element {
    if (!this.tokenHandler.isActive) {
      return <Navigate to="/login" />
    }
    else {
      return <Navigate to="soon" />
    }
  }
}

export default Home
