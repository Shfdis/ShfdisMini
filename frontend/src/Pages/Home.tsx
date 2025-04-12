
import React, { Component, JSX } from 'react'
import { Navigate } from 'react-router-dom'

export class Home extends Component {
  constructor(props: any) {
    super(props)

  }
  render(): JSX.Element {
    return (
      <Navigate to="/soon" />
    )
  }
}

export default Home
