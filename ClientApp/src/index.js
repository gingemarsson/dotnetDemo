import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';

// A single square in the grid
function Square(props) {
    return (
        <button
            onClick={props.onClick}
            className={"status-" + props.status}>
        </button>
    );
}

// The whole grid of squares
class Grid extends React.Component {
    renderSquare(x, y, status, handleClick) {
        return <Square
            status={status}
            onClick={() => handleClick(x, y)}
        />
    }

    render() {
        return (
            <div className="grid">
                {this.props.configuration.grid.map((row, y) => {
                    return (
                        <div className="row">
                            {row.map((cell, x) => {
                                return this.renderSquare(x, y, cell, this.props.onClick)
                            })}
                        </div>
                    )
                })}
            </div>
        );
    }
}

// The controls to save a configuration
class Controls extends React.Component {
    constructor(props) {
        super(props);
        this.state = { name: '' };
    }

    handleChange(event) {
        this.setState({ name: event.target.value });
    }

    render() {
        return (
            <div className="controls">
                <input type="text" placeholder="Name" name="name" onChange={(e) => this.handleChange(e)} onKeyDown={(e) => { if (e.keyCode == 13) { this.props.onClick(this.state.name) }}} />
                <input type="submit" value="Save" className="btnSave" disabled={!this.state.name} onClick={() => this.props.onClick(this.state.name)} />
            </div>
        );
    }
}

// The list of available configurations
class List extends React.Component {
    render() {
        if (this.props.list.length > 0) {
            return (
                <div className="list"><h1>Saved configurations</h1><ul>
                    {this.props.list.map((configuration) => {
                        return (
                            <li className="listItem">
                                <span className="name">{configuration.name}</span>
                                <div className="listControls">
                                    <button className="btnApply" onClick={() => this.props.onApplyClick(configuration.id)}>Apply</button>
                                    <button className="btnDelete" onClick={() => this.props.onDeleteClick(configuration.id)}>Delete</button>
                                </div>
                            </li>
                        )
                    })}
                </ul></div>
            );
        }
        else {
            return (
                <div className="list">
                    <h1>Saved configurations</h1>
                    <span className="noList">Save a configuration and it will appear here</span>
                </div>
            )
        }
    }
}

// The main component which stores the state
class Main extends React.Component {
    constructor(props) {
        super(props)

        this.state = {
            currentConfiguration: this.createEmptyConfiguration(), // The current configuration which is beeing edited
            configurationList: [] // The list of available configurations
        };

        this.getList(); // Download the list from the server upon initialization
    }

    // Returns an empty configuration, i.e. a configuration with default values.
    createEmptyConfiguration() {
        return {
            id: null,
            name: null,
            grid: [
                ["Default", "Default", "Default", "Default", "Default", "Default"],
                ["Default", "Default", "Default", "Default", "Default", "Default"],
                ["Default", "Default", "Default", "Default", "Default", "Default"],
                ["Default", "Default", "Default", "Default", "Default", "Default"],
                ["Default", "Default", "Default", "Default", "Default", "Default"],
                ["Default", "Default", "Default", "Default", "Default", "Default"],
            ]
        }
    }

    // Handles the click on a square. Sets the state to a copy with a grid 
    // where the clicked square has a modified status.
    handleSquareClick(x, y) {
        const grid = this.state.currentConfiguration.grid.slice();

        if (grid[y][x] == "Default")
            grid[y][x] = "Ok";
        else if (grid[y][x] == "Ok")
            grid[y][x] = "Error";
        else if (grid[y][x] == "Error")
            grid[y][x] = "Default";
        else { alert("Error") }

        this.setState({
            currentConfiguration: {
                id: this.state.currentConfiguration.id,
                name: this.state.currentConfiguration.name,
                grid: grid
            },
            configurationList: this.state.configurationList
        });
    }

    // Handle clicks on the save button.
    handleSaveClick(name) {
        var configuration = {
            name: name.slice(),
            grid: this.state.currentConfiguration.grid.slice(),
        };
        this.saveConfiguration(configuration)
    }

    // Handle clicks on the apply button.
    handleApplyClick(id) {
        this.setState({
            currentConfiguration: this.state.configurationList.find((elem) => (elem.id == id)),
            configurationList: this.state.configurationList
        });
    }

    // Handle clicks on the delete button.
    handleDeleteClick(id) {
        this.deleteConfiguration(id)
    }

    render() {
        return (
            <div>
                <Grid
                    configuration={this.state.currentConfiguration}
                    onClick={(x, y) => this.handleSquareClick(x, y)}
                />
                <Controls
                    onClick={(name) => this.handleSaveClick(name)}
                />
                <List
                    list={this.state.configurationList}
                    onApplyClick={(id) => this.handleApplyClick(id)}
                    onDeleteClick={(id) => this.handleDeleteClick(id)}

                />
            </div>
        );
    }

    // Fetch the list of configurations from the server. Returnes nothing, 
    // but sets the configurationlist in the state asyncronysly when the 
    // server responds.
    getList() {
        fetch('/api/Configuration/')
            .then(res => res.json())
            .then((list) => {
                this.setState({
                    currentConfiguration: this.state.currentConfiguration,
                    configurationList: list
                })
            })
            .catch(console.log)
    };

    // Save the specified configuration on the server.
    saveConfiguration(configuration) {
        fetch('/api/Configuration/', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(configuration)
        })
            .then(() => { this.getList(); })
            .catch(console.log)
    };

    // Delete the configuration with the specified id.
    deleteConfiguration(id) {
        fetch('/api/Configuration/' + id, {
            method: 'DELETE'
        })
            .then(() => { this.getList(); })
            .catch(console.log)
    };
}

// ========================================

ReactDOM.render(
    <Main />,
    document.getElementById('root')
);